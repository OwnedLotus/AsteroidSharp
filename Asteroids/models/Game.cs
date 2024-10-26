using System.Numerics;

using AsteroidSharp.Models;
using AsteroidSharp.Models.Shapes;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp;
public enum GameState
{
    Startup,
    Playing,
    Paused,
    GameOver
}

struct Star
{

    public Vector2 Position { get; set; }
    public float Speed { get; set; }
    public Color Color { get => Speed > .25 ? Color.White : Color.Gray; }
}

public class Game
{
    private static System.Timers.Timer asteroidSpawnTimer = new(1000);
    private static System.Timers.Timer playerShootLockoutTimer = new(500);

    private Player player;
    private List<Asteroid> asteroids;
    private Star[] stars = new Star[50];
    private Random rand = new Random();

    public GameState state = GameState.Startup;
    private (int, int) windowDimensions;

    public float Points { get; private set; } = 0;
    public float MaxPoints { get; private set; } = 0;
    public uint numberOfAsteroids { get; private set; }

    public Game((int, int) dimensions)
    {
        windowDimensions = dimensions;
        asteroids = new List<Asteroid>();

        player = new Player(new Vector2(windowDimensions.Item1 / 3, windowDimensions.Item2 / 3), Vector2.Zero, windowDimensions);
        // handles the spawning of asteroids
        asteroidSpawnTimer.Elapsed += async (sender, e) => await SpawnAnotherAsteroid(Vector2.Zero);

        // handles the shooting of the player
        playerShootLockoutTimer.Elapsed += async (sender, e) => await player.EnableShooting();

        // star generator
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i] = new Star
            {
                Position = new Vector2(rand.Next(0, dimensions.Item1), rand.Next(0, dimensions.Item2)),
                Speed = MathF.Pow(rand.NextSingle(), 1.5f)
            };
        }
    }

    #region Private Methods

    private void SpawnNewLocation(ref Star star)
    {
        if (star.Position.Y < 0) star.Position = new Vector2(windowDimensions.Item1 * rand.NextSingle(), windowDimensions.Item2 - 1);
        if (star.Position.Y > windowDimensions.Item2) star.Position = new Vector2(windowDimensions.Item1 * rand.NextSingle(), 1);
        if (star.Position.X < 0) star.Position = new Vector2(windowDimensions.Item1 - 1, windowDimensions.Item2 * rand.NextSingle());
        if (star.Position.X > windowDimensions.Item1) star.Position = new Vector2(1, windowDimensions.Item2 * rand.NextSingle());
    }

    private void DestroyAsteroid(Asteroid asteroid)
    {
        asteroids.Remove(asteroid);
    }

    private Task SpawnAnotherAsteroid(Vector2 position)
    {
        var attemptedAsteroidSpawn = new Asteroid(windowDimensions, position);
        asteroids.Add(attemptedAsteroidSpawn);

        // check proximity to player as to not spawn on top of the player
        if (Math.Abs(attemptedAsteroidSpawn.Position.X - player.Position.X) < attemptedAsteroidSpawn.Scale ||
                Math.Abs(attemptedAsteroidSpawn.Position.Y - player.Position.Y) < attemptedAsteroidSpawn.Scale)
            DestroyAsteroid(attemptedAsteroidSpawn);

        return Task.CompletedTask;
    }

    private void LaunchGame()
    {
        state = GameState.Playing;
        asteroidSpawnTimer.Enabled = true;
        playerShootLockoutTimer.Enabled = true;
    }

    private void ResetGame()
    {
        player = new Player(new Vector2(windowDimensions.Item1 / 3, windowDimensions.Item2 / 3), Vector2.Zero, windowDimensions);

        asteroids.Clear();

        for (int i = asteroids.Count - 1; i >= 0; i--)
        {
            asteroids.RemoveAt(i);
        }

        if (MaxPoints < Points)
            MaxPoints = Points;

        Points = 0;
        asteroidSpawnTimer.Enabled = true;
        playerShootLockoutTimer.Enabled = true;
    }

    private void DrawLives()
    {
        for (int i = 0; i < player.Lives.Count; i++)
            player.Lives.Peek().DrawShape();
    }

    private void DrawStartMenu()
    {
        string title = "Asteroids";
        string startText = "Start Playing? (Press Enter to Begin)";
        string quitText = "Quit? (Press Q to Quit)";

        int x_title_pos = (windowDimensions.Item1 / 2) - title.Length * 13;
        // Console.WriteLine(x_title_pos);
        int x_start_pos = (windowDimensions.Item1 / 2) - startText.Length * 5;
        int x_quit_pos = (windowDimensions.Item1 / 2) - quitText.Length * 6;


        // Menu showing but no game showing
        Graphics.DrawText(title, x_title_pos, windowDimensions.Item2 / 3, 40, Color.White);
        Graphics.DrawText(startText, x_start_pos, windowDimensions.Item2 / 2, 20, Color.White);
        Graphics.DrawText(quitText, x_quit_pos, windowDimensions.Item2 * 2 / 3, 20, Color.White);
    }

    private void DrawPauseMenu()
    {
        // Still showing the game with no game ticks
        DrawGamePlaying(false);

        string info = $"Total Points: {Points}";
        string cont = "Continue? (Press Enter)";
        string q = "Quit? (Press Q)";

        int x_info_pos = windowDimensions.Item1 / 2 - info.Length * 6;
        int x_cont_pos = windowDimensions.Item1 / 2 - cont.Length * 6;
        int x_q_pos = windowDimensions.Item1 / 2 - q.Length * 8;

        Graphics.DrawText(info, x_info_pos, windowDimensions.Item2 / 3, 20, Color.White);
        Graphics.DrawText(cont, x_cont_pos, windowDimensions.Item2 / 3, 20, Color.White);
        Graphics.DrawText(q, x_q_pos, windowDimensions.Item2 / 3, 20, Color.White);
    }

    private void DrawPoints()
    {
        Graphics.DrawText("Points: " + Points, 0, 10, 20, Color.White);
    }

    private void DrawGameOverMenu()
    {
        // Still showing the game with no game ticks
        DrawGamePlaying(false);

        string gameOverText = "Game Over!";
        string playAgainText = "Play Again? (Press Enter)";
        string quitText = "Quit Game? (Press Q)";

        int x_gameOver_pos = (windowDimensions.Item1 / 2) - gameOverText.Length * 6;
        int x_playAgain_pos = (windowDimensions.Item1 / 2) - playAgainText.Length * 4;
        int x_quit_pos = (windowDimensions.Item1 / 2) - quitText.Length * 4;

        Graphics.DrawText(gameOverText, x_gameOver_pos, windowDimensions.Item2 / 3, 40, Color.White);
        Graphics.DrawText(playAgainText, x_playAgain_pos, windowDimensions.Item2 / 2, 20, Color.White);
        Graphics.DrawText(quitText, x_quit_pos, windowDimensions.Item2 * 2 / 3, 20, Color.White);

    }

    #endregion

    #region Public Methods

    public void StartGame()
    {
        if (Input.IsKeyPressed(KeyboardKey.Enter))
        {
            LaunchGame();
        }

        QueryQuitGame();
    }


    public void UpdateGame()
    {
        float deltaTime = Time.GetFrameTime();

        player.UpdatePlayer(deltaTime);
        MoveAsteroids(deltaTime);
        Collide(deltaTime);

        // handling pausing
        if (Input.IsKeyPressed(KeyboardKey.Enter) && state == GameState.Playing) state = GameState.Paused;
        if (Input.IsKeyPressed(KeyboardKey.Enter) && state == GameState.Paused) state = GameState.Playing;

        MoveStars();

    }

    private void MoveStars()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].Position -= player.Heading * stars[i].Speed;
            SpawnNewLocation(ref stars[i]);
        }
    }

    private void Collide(float deltaTime)
    {
        for (int i = 0; i < player.activeBullets.Count; i++)
        {
            var currentBullet = player.activeBullets[i];

            if (currentBullet.Position.X < 0 ||
                currentBullet.Position.Y < 0 ||
                currentBullet.Position.X > windowDimensions.Item1 ||
                currentBullet.Position.Y > windowDimensions.Item2)
                player.DespawnBullet(currentBullet);
            else
            {
                currentBullet.Move(deltaTime);
                var collidedAsteroid = asteroids.FirstOrDefault(asteroid => asteroid.CheckCollisions(currentBullet.Corners));

                if (collidedAsteroid is not null)
                {
                    player.DespawnBullet(currentBullet);

                    if (collidedAsteroid.State == AsteroidState.Large)
                    {
                        SpawnAnotherAsteroid(collidedAsteroid.Position);
                        SpawnAnotherAsteroid(collidedAsteroid.Position);
                    }

                    Points += MathF.Round(100 * collidedAsteroid.Speed * 10 / collidedAsteroid.Scale);

                    DestroyAsteroid(collidedAsteroid);
                }
            }
        }
    }

    private void MoveAsteroids(float deltaTime)
    {
        // move all Asteroids
        for (int i = 0; i < asteroids.Count; i++)
        {
            // TODO Some asteroids are able to be collided with twice

            var currentAsteroid = asteroids[i];

            if (currentAsteroid.Position.X < 0 ||
                currentAsteroid.Position.Y < 0 ||
                currentAsteroid.Position.X > windowDimensions.Item1 ||
                currentAsteroid.Position.Y > windowDimensions.Item2)
                DestroyAsteroid(currentAsteroid);
            else
                currentAsteroid.Move(deltaTime);

            // check collisions on player and asteroids
            if (currentAsteroid.CheckCollisions(player.Corners))
            {
                // Runs Game over
                if (player.Lives.Count == 0)
                {
                    asteroidSpawnTimer.Enabled = false;
                    playerShootLockoutTimer.Enabled = false;
                    state = GameState.GameOver;
                    RunGameOver();
                }
                else
                    player.RemoveLife();

                // Check for asteroids in the area and respawn player
                Random rng = new();
                Asteroid? collidedAsteroids;
                do
                {
                    player.RespawnPlayer(new Vector2(rng.Next(0, windowDimensions.Item1), rng.Next(0, windowDimensions.Item2)));
                    player.UpdatePlayer(0f);
                    collidedAsteroids = asteroids.FirstOrDefault(asteroid => asteroid.CheckCollisions(player.Corners));
                } while (collidedAsteroids is not null);
            }
        }
    }

    public void DrawGame()
    {
        switch (state)
        {
            case GameState.Startup:
                DrawStartMenu();
                break;
            case GameState.Playing:
                DrawGamePlaying();
                break;
            case GameState.Paused:
                DrawPauseMenu();
                break;
            case GameState.GameOver:
                DrawGameOverMenu();
                break;
        }

        DrawStars();
    }

    private void DrawStars()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            Graphics.DrawCircleV(stars[i].Position, 1, Color.White);
        }
    }

    private void DrawGamePlaying(bool showPoints = true)
    {

        if (showPoints)
            DrawPoints();
        DrawLives();

        player.DrawPlayer();

        for (int i = 0; i < asteroids.Count; i++)
        {
            asteroids[i].DrawAsteroid();
        }
    }

    public void PauseMenu()
    {
        if (Input.IsKeyPressed(KeyboardKey.Enter))
            state = GameState.Playing;

        QueryQuitGame();
    }



    public void RunGameOver()
    {
        if (Input.IsKeyPressed(KeyboardKey.Enter))
        {
            state = GameState.Playing;
            ResetGame();
        }
    }



    #endregion

    private void QueryQuitGame()
    {
        if (Input.IsKeyPressed(KeyboardKey.Q))
        {
            asteroidSpawnTimer.Dispose();
            playerShootLockoutTimer.Dispose();
            Environment.Exit(0);
        }
    }
}

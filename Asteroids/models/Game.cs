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

public class Game
{
    private static System.Timers.Timer asteroidSpawnTimer = new(1000);
    private static System.Timers.Timer playerShootLockoutTimer = new(500);

    private List<Triangle> lives = new()
    {
        new Triangle(new Vector2(6, 4), Vector2.UnitY, Color.White),
        new Triangle(new Vector2(6, 4), Vector2.UnitY, Color.White),
        new Triangle(new Vector2(6, 4), Vector2.UnitY, Color.White),
        new Triangle(new Vector2(6, 4), Vector2.UnitY, Color.White),
    };
    private Player player;
    private List<Asteroid> asteroids;

    public GameState state = GameState.Startup;
    private (int, int) windowDimensions;

    public float Points { get; private set; } = 0;
    public float MaxPoints { get; private set; } = 0;
    public uint numberOfAsteroids { get; private set; }

    public Game((int, int) dimensions)
    {
        windowDimensions = dimensions;
        player = new Player(new Vector2(windowDimensions.Item1 / 3, windowDimensions.Item2 / 3), Vector2.Zero, windowDimensions);
        asteroids = new List<Asteroid>();

        for (int i = 0; i < lives.Count(); i++)
        {
            lives[i].UpdateShape(new Vector2(10 * i + 10, 40));
        }

        // handles the spawning of asteroids
        asteroidSpawnTimer.Elapsed += async (sender, e) => await SpawnAnotherAsteroid(Vector2.Zero);

        // handles the shooting of the player
        playerShootLockoutTimer.Elapsed += async (sender, e) => await player.EnableShooting();
    }

    #region Private Methods

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

        for (int i = asteroids.Count - 1; i >= 0 ; i--)
        {
            asteroids.RemoveAt(i);
        }

        if (MaxPoints < Points)
            MaxPoints = Points;
        
        Points = 0;
        player.Lives = 3;
    }

    private void DrawLives()
    {
        foreach (var life in lives)
        {
            life.DrawShape();
        }
    }

    private void DrawStartMenu()
    {
        string title = "Asteroids";
        string startText = "Start Playing? (Press Enter to Begin)";
        string quitText = "Quit? (Press Q to Quit)";

        int x_title_pos = (windowDimensions.Item1 / 2) - title.Length * 13;
        // Console.WriteLine(x_title_pos);
        int x_start_pos =  (windowDimensions.Item1 / 2) - startText.Length * 5;
        int x_quit_pos = (windowDimensions.Item1 / 2) - quitText.Length * 6;


        // Menu showing but no game showing
        Graphics.DrawText(title, x_title_pos , windowDimensions.Item2 / 3, 40, Color.White );
        Graphics.DrawText(startText, x_start_pos ,windowDimensions.Item2 /2, 20, Color.White);
        Graphics.DrawText(quitText, x_quit_pos, windowDimensions.Item2 * 2/3, 20, Color.White);
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

        Graphics.DrawText(info, x_info_pos, windowDimensions.Item2 / 3, 20, Color.White );
        Graphics.DrawText(cont, x_cont_pos, windowDimensions.Item2 / 3, 20, Color.White );
        Graphics.DrawText(q, x_q_pos, windowDimensions.Item2 / 3, 20, Color.White );
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
        if(Input.IsKeyPressed(KeyboardKey.Enter))
        {
            LaunchGame();
        }

        QueryQuitGame();
    }


    public void UpdateGame()
    {
        float deltaTime = Time.GetFrameTime();

        player.UpdatePlayer(deltaTime);

        // move all Asteroids
        for (int i = 0; i < asteroids.Count; i++)
        {
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
                if (player.Lives == 0)
                {
                    asteroidSpawnTimer.Enabled = false;
                    playerShootLockoutTimer.Enabled = false;
                    state = GameState.GameOver;
                    RunGameOver();
                }

                // Check for asteroids in the area and respawn player
                Random rng = new();
                Asteroid? collidedAsteroids;
                do
                {
                    player.RespawnPlayer(new Vector2(rng.Next(0, windowDimensions.Item1), rng.Next(0, windowDimensions.Item2)));
                    player.UpdatePlayer(0f);
                    collidedAsteroids = asteroids.FirstOrDefault(asteroid => asteroid.CheckCollisions(player.Corners));
                } while (collidedAsteroids is not null);

                lives.RemoveAt(player.Lives);
                player.Lives--;
            }
        }


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

        // handling pausing
        if(Input.IsKeyPressed(KeyboardKey.Enter) && state == GameState.Playing) state = GameState.Paused;
        if(Input.IsKeyPressed(KeyboardKey.Enter) && state == GameState.Paused) state = GameState.Playing;
    
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
    }

    private void DrawGamePlaying(bool showPoints = true)
    {
        if (showPoints)
            DrawPoints();
        DrawLives();

        player.DrawPlayer();

        foreach (var asteroid in asteroids)
        {
            asteroid.DrawAsteroid();
        }
    }
    
    public void PauseMenu()
    {
        if(Input.IsKeyPressed(KeyboardKey.Enter))
            state = GameState.Playing;

        QueryQuitGame();
    }

    

    public void RunGameOver()
    {
        if(Input.IsKeyPressed(KeyboardKey.Enter))
        {
            state = GameState.Playing;

        }
    }



    #endregion


    private void QueryQuitGame()
    {
        if(Input.IsKeyPressed(KeyboardKey.Q))
        {
            asteroidSpawnTimer.Dispose();
            playerShootLockoutTimer.Dispose();
            Environment.Exit(0);
        }
    }
}

using System.Numerics;

using AsteroidSharp.Models;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Colors;
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
    private static System.Timers.Timer timer = new(1000);

    private Player player;
    private List<Asteroid> asteroids;

    public GameState state = GameState.Startup;
    private (uint, uint) windowDimensions;

    public float points { get; private set; } = 0;
    public uint numberOfAsteroids { get; private set; }

    public Game((uint, uint) dimensions)
    {
        windowDimensions = dimensions;
        player = new Player(new Vector2(windowDimensions.Item1 / 3, windowDimensions.Item2 / 3), new Vector2(0, 0), dimensions);
        asteroids = new();

        // handles the spawning of asteroids
        timer.Enabled = true;
        timer.Elapsed += async (sender, e) => await SpawnAnotherAsteroid();
    }

    #region Private Methods

    private void DestroyAsteroid(Asteroid asteroid)
    {
        asteroids.Remove(asteroid);
    }

    private Task SpawnAnotherAsteroid()
    {
        var attemptedAsteroidSpawn = new Asteroid(windowDimensions);

        asteroids.Add(attemptedAsteroidSpawn);

        // check proximity to player as to not spawn on top of the player
        if (Math.Abs(attemptedAsteroidSpawn.Position.X - player.Position.X) < attemptedAsteroidSpawn.Scale ||
                Math.Abs(attemptedAsteroidSpawn.Position.Y - player.Position.Y) < attemptedAsteroidSpawn.Scale)
            DestroyAsteroid(attemptedAsteroidSpawn);


        return Task.CompletedTask;
    }

    #endregion

    #region Public Methods

    public void LaunchGame()
    {
        state = GameState.Playing;
    }

    public void UpdateGame()
    {
        float deltaTime = Time.GetFrameTime();

        // move Player
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
                RunGameOver();
        }


        for (int i = 0; i < player.activeBullets.Count; i++)
        {
            var currentBullet = player.activeBullets[i];

            if (currentBullet.Position.X < 0 ||
                currentBullet.Position.Y < 0 ||
                currentBullet.Position.X > windowDimensions.Item1 ||
                currentBullet.Position.Y > windowDimensions.Item2)
                player.DespawnBullet(player.activeBullets[i]);
            else
            {
                currentBullet.Move(deltaTime);

                var collidedAsteroid = asteroids.FirstOrDefault(asteroids => asteroids.CheckCollisions(currentBullet.Corners));

                if (collidedAsteroid is not null)
                {
                    player.DespawnBullet(player.activeBullets[i]);
                    DestroyAsteroid(collidedAsteroid);
                    points += MathF.Round(100 * 10 / collidedAsteroid.Scale);
                }
            }
        }

        // first attempt to add pausing to the game
        if (Input.IsKeyDown(KeyboardKey.Enter) && state != GameState.Paused) state = GameState.Paused;
        if (Input.IsKeyDown(KeyboardKey.Enter) && state != GameState.Playing) state = GameState.Playing;
    }

    public void DrawGame()
    {
        Raylib_CSharp.Rendering.Graphics.DrawText("Points: " + points, (int)windowDimensions.Item1 / 2, 10, 20, Color.White);

        player.DrawPlayer();

        foreach (var asteroid in asteroids)
        {
            asteroid.DrawAsteroid();
        }
    }

    public void RunGameOver()
    {
        timer.Dispose();
        state = GameState.GameOver;

    }

    #endregion

}

using System.Numerics;

using AsteroidSharp.Models;
using Raylib_CSharp.Interact;
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
    private List<Asteroid> destroyedAsteroids;

    public GameState state = GameState.Startup;
    private (uint, uint) windowDimensions;

    public uint points { get; private set; } = 0;
    public uint numberOfAsteroids { get; private set; }

    public Game((uint, uint) dimensions)
    {
        windowDimensions = dimensions;
        player = new Player(new Vector2(windowDimensions.Item1 / 3, windowDimensions.Item2 / 3), new Vector2(0, 0), dimensions);
        asteroids = new();
        destroyedAsteroids = new();
        timer.Enabled = true;
        timer.Elapsed += async (sender, e) => await SpawnAnotherAsteroid();
        //asteroids.Add(new Asteroid(windowDimensions, 100));
    }

    #region Private Methods

    private void DestroyAsteroid(Asteroid asteroid)
    {
        asteroids.Remove(asteroid);
        asteroid.State = ActorState.Destroyed;
        destroyedAsteroids.Add(asteroid);
    }

    private Task SpawnAnotherAsteroid()
    {
        asteroids.Add(new Asteroid(windowDimensions, 100));
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

                // Index out of range
                var collidedAsteroid = asteroids.FirstOrDefault(asteroids => asteroids.CheckCollisions(currentBullet.Corners));

                if (collidedAsteroid is not null)
                {
                    player.DespawnBullet(player.activeBullets[i]);
                    DestroyAsteroid(collidedAsteroid);
                }
            }
        }

        if (Input.IsKeyDown(KeyboardKey.Enter) && state != GameState.Paused) state = GameState.Paused;
        if (Input.IsKeyDown(KeyboardKey.Enter) && state != GameState.Playing) state = GameState.Playing;
    }

    public void DrawGame()
    {
        player.DrawPlayer();

        foreach (var asteroid in asteroids)
        {
            asteroid.DrawAsteroid();
        }
    }

    public void RunGameOver()
    {
        timer.Dispose();
    }

    #endregion

}

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
    }

    #region Private Methods

    private void DestroyAsteroid(Asteroid asteroid)
    {
        Console.WriteLine("Destroying the asteroid");
        asteroids.Remove(asteroid);
        asteroid.State = ActorState.Destroyed;
        destroyedAsteroids.Add(asteroid);
    }

    #endregion

    #region Public Methods

    public void LaunchGame()
    {
        state = GameState.Playing;
        asteroids.Add(new Asteroid(windowDimensions));
    }

    public void UpdateGame()
    {
        float deltaTime = Time.GetFrameTime();

        // move Player
        player.UpdatePlayer(deltaTime);

        // move all Asteroids
        foreach (var asteroid in asteroids)
        {
            asteroid.Move(deltaTime);
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
                var collidedAsteroid = asteroids.SingleOrDefault(asteroids => asteroids.CheckCollisions(currentBullet.Corners));

                if (collidedAsteroid is not null)
                {
                    Console.WriteLine("Asteroid Collided");
                    player.DespawnBullet(player.activeBullets[i]);
                    DestroyAsteroid(collidedAsteroid);
                }
            }
        }

        if (Input.IsKeyDown(KeyboardKey.Enter) && state != GameState.Paused) state = GameState.Paused;
        if (Input.IsKeyDown(KeyboardKey.Enter) && state != GameState.Playing) state = GameState.Playing;
    }

    public void SpawnAnotherAsteroid()
    {
        asteroids.Add(new Asteroid(windowDimensions));
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
        throw new NotImplementedException();
    }

    #endregion

}

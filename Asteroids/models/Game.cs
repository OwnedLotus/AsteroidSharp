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
    private Player player;
    private List<Asteroid>? asteroids;

    public GameState state = GameState.Startup;
    private (uint, uint) windowDimensions;

    public uint points { get; private set; } = 0;
    public uint numberOfAsteroids { get; private set; }

    public Game((uint, uint) dimensions)
    {
        windowDimensions = dimensions;
        player = new Player(new Vector2(windowDimensions.Item1 / 2, windowDimensions.Item2 / 2), new Vector2(0, 0), dimensions);
    }

    #region Private Methods

    private void CheckCollisions()
    {
        // types of collisions
        // 1. bullet <-> asteroid
        // 2. asteroid <-> player
        // 3. bullet <-> player <-- may have issue with init laser spawn
        // foreach (var bullet in player.activeBullets)
        // {
        //     bullet.CollisionCheck(player.Corners);
        // }
        



        // 4. enemy <-> bullet
        // 5. enemy <-> asteroid
        // 6. enemy <-> player

    }

    #endregion

    #region Public Methods

    public void LaunchGame()
    {
        asteroids = new List<Asteroid>();
        state = GameState.Playing;
        asteroids.Add(new Asteroid(windowDimensions));
    }

    public void UpdateGame()
    {
        float deltaTime = Time.GetFrameTime();

        // move Player
        player.UpdatePlayer(deltaTime);

        // move all Asteroids
        if (asteroids is not null)
        {
            foreach (var asteroid in asteroids)
            {
                asteroid.Move(deltaTime);
            }
        }

        if (Input.IsKeyDown(KeyboardKey.Enter) && state != GameState.Paused) state = GameState.Paused;
        if (Input.IsKeyDown(KeyboardKey.Enter) && state != GameState.Playing) state = GameState.Playing;
    }

    public void SpawnAnotherAsteroid()
    {
        asteroids?.Add(new Asteroid(windowDimensions));
    }

    public void DrawGame()
    {
        player.DrawPlayer();

        if (asteroids is not null)
        {
            foreach (var asteroid in asteroids)
            {
                asteroid.DrawAsteroid();
            }
        }
    }

    public void RunGameOver()
    {
        throw new NotImplementedException();
    }

    #endregion

}

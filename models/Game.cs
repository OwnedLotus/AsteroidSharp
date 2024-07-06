using System.Numerics;
using AsteroidSharp.Models;

public enum GameState
{
    Startup,
    Playing,
    GameOver
}

public class Game
{
    private Player? player;
    private List<Asteroid>? asteroids;
    public GameState state = GameState.Startup;
    private (int, int) windowDimensions;

    public uint points { get; private set; } = 0;
    public uint numberOfAsteroids { get; private set; }


    public Game((int, int) dimensions)
    {
        windowDimensions = dimensions;
        player = new Player(new Vector2(windowDimensions.Item1 / 2, windowDimensions.Item2 / 2), new Vector2(0, 0));
    }


    #region Private Methods
    #endregion

    #region Public Methods

    public void LaunchGame()
    {
        asteroids = new List<Asteroid>();
        state = GameState.Playing;
    }


    public void RunGame()
    {
        // move Player
        player?.UpdatePlayer();

        // move Asteroids
        if (asteroids is not null)
        {
            foreach (var asteroid in asteroids)
            {
                asteroid.Move();
            }
        }
    }

    public void SpawnAnotherAsteroid()
    {
        asteroids?.Add(new Asteroid());
    }

    public void DrawGame()
    {
        player?.DrawPlayer();
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

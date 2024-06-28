using System.Numerics;
using AsteroidSharp.Models;
using AsteroidSharp.Models.Shapes;
using Raylib_CSharp.Interact;

public enum GameState
{
    Startup,
    Playing,
    GameOver
}

public class Game
{
    private Player player;
    private List<Asteroid> asteroids;
    public GameState state = GameState.Startup;

    public uint points { get; private set; } = 0;
    public uint numberOfAsteroids { get; private set; }


    public Game()
    {
        player = new Player(new Vector2(0, 0), new Vector2(0f, 0f), new Square());
        asteroids = new List<Asteroid>();
    }


    #region Private Methods
    #endregion

    #region Public Methods

    public void RunGame()
    {
        // move Player
        player.MovePlayer();

        // move Asteroids
        foreach (var asteroid in asteroids)
        {
            asteroid.Move();
        }
    }

    public void SpawnAnotherAsteroid()
    {
        throw new NotImplementedException();
    }

    public void DrawGame()
    {
        player.DrawPlayer();
        foreach (var asteroid in asteroids)
        {
            asteroid.DrawAsteroid();
        }
    }

    public void UpdatePositions(KeyboardKey key)
    {
        player.MovePlayer();

        foreach (var asteroid in asteroids)
        {
            asteroid.Move();
        }
    }

    #endregion

}

using AsteroidSharp.Models;
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
        player = new Player(new Position(0,0), new System.Numerics.Vector2(0f,0f));
        asteroids = new List<Asteroid>();
    }


#region Private Methods
#endregion

#region Public Methods

    public void SpawnAnotherAsteroid()
    {
        throw new NotImplementedException();
    }

    public void LaunchGame()
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
        player.MovePlayer(key);

        foreach (var asteroid in asteroids)
        {
            asteroid.Move();
        }
    }

#endregion

}
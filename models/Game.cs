public enum GameState
{
    Startup,
    Playing,
    GameOver
}

public class Game(uint points, GameState state, uint numAsteroids)
{
    public void LaunchGame()
    {
        throw new NotImplementedException();
    }

    public void DrawGame()
    {
        throw new NotImplementedException();
    }

    public void UpdatePositions()
    {
        throw new NotImplementedException();
    }
}
namespace AsteroidSharp.Models;

public struct Position(int x, int y)
{
    public int X { get; private set; } = x;
    public int Y { get; private set; } = y;
}

namespace AsteroidSharp.Models;

public struct Position(int x, int y)
{
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
}

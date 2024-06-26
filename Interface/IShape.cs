using AsteroidSharp.Models;

public interface IShape
{
    public Position Pos { get; set; }
    public int[] Bounds { get; set; }

    public void DrawShape();
}

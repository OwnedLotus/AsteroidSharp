using AsteroidSharp.Models;

public interface IShape
{
    public int[] Bounds { get; set; }

    public void DrawShape(int x, int y);
}

using System.Numerics;
using AsteroidSharp.Models;

public interface IShape
{


    public int[] Bounds { get; }

    public void DrawShape(Vector2 pos);
}

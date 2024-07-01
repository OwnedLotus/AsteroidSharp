using System.Numerics;
using AsteroidSharp.Models;

public interface IShape
{


    public int[] Bounds { get; }

    public void DrawShape(Vector2 pos);
    public void RotateShapeClockwise(Vector2 pos, float rotateSpeed);
    public void RotateShapeCounterClockwise(Vector2 pos, float rotateSpeed);
}

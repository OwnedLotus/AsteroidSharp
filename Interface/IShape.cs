using System.Numerics;
using AsteroidSharp.Models;

public interface IShape
{
    public Vector2 UpdateShape(Vector2 pos);
    public void DrawShape();
    public Vector2 RotateShape(Vector2 pos, float rotateSpeed);
}

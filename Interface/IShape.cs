using System.Numerics;
using AsteroidSharp.Models;
using Raylib_CSharp.Colors;

public interface IShape
{
    Color Color { get; set; }

    public Vector2 UpdateShape(Vector2 pos);
    public void DrawShape();
    public Vector2 RotateShape(Vector2 pos, float rotateSpeed);
}

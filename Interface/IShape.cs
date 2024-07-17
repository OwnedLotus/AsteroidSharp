using System.Numerics;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Transformations;

public interface IShape
{
    Color ShapeColor { get; }
    public Vector2[] Corners { get; }
    public Raylib_CSharp.Transformations.Rectangle Rectangle { get; }

    public Vector2 UpdateShape(Vector2 pos);
    public void DrawShape();
    public Vector2 RotateShape(Vector2 pos, float rotateSpeed);
}

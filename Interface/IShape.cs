using System.Numerics;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Transformations;

public interface IShape
{
    Color ShapeColor { get; }
    public Vector2[] Corners { get; }

    public Rectangle Rect { get; }

    public Vector2 UpdateShape(Vector2 pos);
    public void DrawShape();
    public Vector2 RotateShape(Vector2 pos, float rotateSpeed);
}

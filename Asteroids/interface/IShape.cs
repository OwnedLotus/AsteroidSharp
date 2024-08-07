using System.Numerics;
using Raylib_CSharp.Colors;

public interface IShape
{
    Color ShapeColor { get; }
    public Vector2[] Corners { get; }

    public Vector2 UpdateShape(Vector2 pos);
    public void DrawShape();
    public Vector2 RotateShape(Vector2 pos, float rotateSpeed);

}

using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;


namespace AsteroidSharp.Models.Shapes;

class Circle : IShape
{
    private Vector2 position;
    private Color _color;

    private int _bound;

    public Color Color { get => _color; set => _color = value; }

    public Circle(int bound)
    {
        _bound = bound;
    }

    public Vector2 RotateShape(Vector2 pos, float rotateSpeed)
    {
        // not useful
        return Vector2.Normalize(position);
    }

    public Vector2 UpdateShape(Vector2 pos)
    {
        position = pos;
        return Vector2.Normalize(position);
    }

    public void DrawShape()
    {
        Graphics.DrawCircleLinesV(position, _bound, _color);
    }
}

using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Circle : IShape
{
    private Vector2 _position;
    private Color _color;
    private int _bound;

    public Color ShapeColor { get => _color; set => _color = value; }
    public Vector2[] Corners { get => throw new NotImplementedException(); private set => throw new NotImplementedException(); }

    public Circle(int bound, Color color)
    {
        _bound = bound;
        _color = color;
    }

    public Vector2 RotateShape(Vector2 pos, float rotateSpeed)
    {
        // not useful but for polymorphism this will just return the normalized position "Heading"
        return Vector2.Normalize(_position);
    }

    public Vector2 UpdateShape(Vector2 pos)
    {
        _position += pos;
        return Vector2.Normalize(_position);
    }

    public void DrawShape()
    {
        Graphics.DrawCircleLinesV(_position, _bound, _color);
    }

    public bool Collision(IEnumerable<Vector2> points)
    {
        throw new NotImplementedException();
    }
}

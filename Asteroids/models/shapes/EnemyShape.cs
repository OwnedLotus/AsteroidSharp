using System.Numerics;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;

namespace AsteroidSharp.Models.Shapes;


class EnemyShape : IShape
{
    private Vector2 _pos;
    private Vector2 _bounds;
    private Color _color;


    public Vector2[] Corners { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Color ShapeColor { get => _color; set => _color = value; }
    public ActorState State { get; set; }

    public EnemyShape(Vector2 bounds, Color color, Vector2 pos)
    {
        _bounds = bounds;
        _color = color;
        _pos = pos;
    }

    public Vector2 UpdateShape(Vector2 position)
    {
        _pos = position;

        return Vector2.Normalize(_pos);
    }

    public void DrawShape()
    {
        // Diamond or rhombus would be more inline with the rest of the program

        Graphics.DrawEllipseLines((int)_pos.X, (int)_pos.Y, (int)_bounds.X, (int)_bounds.Y, _color);
    }

    public Vector2 RotateShape(Vector2 position, float rotation)
    {
        // I think that all enemy movement will be upright
        return Vector2.Zero;
    }

    public bool Collision(Vector2[] points)
    {
        throw new NotImplementedException();
    }
}

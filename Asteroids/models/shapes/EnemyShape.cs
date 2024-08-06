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
        // I was thinking to have the enemies as ovals if I am not going to implement
        // them as actual meshes and render an actual image

        Graphics.DrawEllipseLines((int)_pos.X, (int)_pos.Y, (int)_bounds.X, (int)_bounds.Y, _color);
    }

    public Vector2 RotateShape(Vector2 position, float rotation)
    {
        // I think that all enemy movement will be upright
        return Vector2.Zero;
    }

    public bool Collision(IEnumerable<Vector2>? boundaries)
    {
        throw new NotImplementedException();
    }
}

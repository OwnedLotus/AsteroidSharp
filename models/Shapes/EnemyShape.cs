using System.Numerics;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models.Shapes;


class EnemyShape : IShape
{
    private Color _color;

    public Vector2[] Corners { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Color ShapeColor { get => _color; set => _color = value; }

    public Raylib_CSharp.Transformations.Rectangle Rectangle => throw new NotImplementedException();

    public EnemyShape()
    {

    }

    public Vector2 UpdateShape(Vector2 position)
    {
        throw new NotImplementedException();
    }

    public void DrawShape()
    {
        // I was thinking to have the enemies as ovals if I am not going to implement
        // them as actual meshes and render an actual image

        throw new NotImplementedException();

    }

    public Vector2 RotateShape(Vector2 position, float rotation)
    {
        throw new NotImplementedException();
    }

    public bool Collision(IEnumerable<Vector2> boundaries)
    {
        throw new NotImplementedException();
    }
}

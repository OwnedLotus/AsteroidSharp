using System.Numerics;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models.Shapes;


class EnemyShape : IShape
{

    public Vector2[] Corners { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Color Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


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
}

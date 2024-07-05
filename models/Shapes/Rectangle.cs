using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Rectangle : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;

    private int[] _bounds;

    public Vector2[] Corners { get => globalCoordinates; private set => globalCoordinates = value; }


    public Rectangle(int[] bounds)
    {
        _bounds = bounds;
        globalCoordinates = new Vector2[4];
        localCoordinates = new Vector2[4];
    }

    public Color Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void DrawShape()
    {
        throw new NotImplementedException();
    }

    public Vector2 RotateShape(Vector2 pos, float rotateSpeed)
    {
        throw new NotImplementedException();
    }

    public Vector2 UpdateShape(Vector2 pos)
    {
        throw new NotImplementedException();
    }
}

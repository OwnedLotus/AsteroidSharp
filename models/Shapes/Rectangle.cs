using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Rectangle : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;

    private int[] _bounds;

    public Rectangle(int[] bounds)
    {
        _bounds = bounds;
    }

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

using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Square : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;


    public Vector2[] Corners { get => globalCoordinates; private set => globalCoordinates = value; }

    private int _bound;

    public Square(int bound)
    {
        _bound = bound;
        localCoordinates = new Vector2[4];
        globalCoordinates = new Vector2[4];
    }

    public Color Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void DrawShape()
    {
        // Graphics.DrawRectangle();
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

using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;


namespace AsteroidSharp.Models.Shapes;

class Circle : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;


    private int _bound;

    public Color Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Circle(int bound)
    {
        _bound = bound;
    }

    public Vector2 RotateShape(Vector2 pos, float rotateSpeed)
    {
        throw new NotImplementedException();
    }

    public Vector2 UpdateShape(Vector2 pos)
    {
        throw new NotImplementedException();
    }

    public void DrawShape()
    {
        throw new NotImplementedException();
    }
}
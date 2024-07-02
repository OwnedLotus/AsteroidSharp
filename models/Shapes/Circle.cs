using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;


namespace AsteroidSharp.Models.Shapes;

class Circle : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;


    private int[] _bounds;

    public int[] Bounds { get => _bounds; private set => _bounds = value; }

    public Circle(int bound)
    {
        Bounds = new int[] { bound };
    }

    public void RotateShape(Vector2 pos, float rotateSpeed)
    {
        throw new NotImplementedException();
    }

    public void UpdateShape(Vector2 pos)
    {
        throw new NotImplementedException();
    }

    public void DrawShape()
    {
        throw new NotImplementedException();
    }
}
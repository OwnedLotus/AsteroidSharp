using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Square : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;
    
    private int[] _bound;

    public int[] Bounds { get => _bound; private set => _bound = value; }

    public Square(int bound)
    {
        Bounds = new int[] { bound };
    }

    public void DrawShape()
    {
        throw new NotImplementedException();
    }

    public void RotateShape(Vector2 pos, float rotateSpeed)
    {
        throw new NotImplementedException();
    }

    public void UpdateShape(Vector2 pos)
    {
        throw new NotImplementedException();
    }
}

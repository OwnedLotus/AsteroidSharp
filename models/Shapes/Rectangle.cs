using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Rectangle : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;

    public int[] Bounds { get; }

    public Rectangle(int[] bounds)
    {
        
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

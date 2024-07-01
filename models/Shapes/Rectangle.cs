using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Rectangle : IShape
{
    public int[] Bounds { get; }

    public Rectangle(int[] bounds)
    {
        
    }

    public void DrawShape(Vector2 pos)
    {
        Graphics.DrawRectangle((int)pos.X, (int)pos.Y, Bounds[0], Bounds[1], Color.Black);
    }

    public void RotateShapeClockwise(Vector2 pos, float rotateSpeed)
    {
        throw new NotImplementedException();
    }

    public void RotateShapeCounterClockwise(Vector2 pos, float rotateSpeed)
    {
        throw new NotImplementedException();
    }
}

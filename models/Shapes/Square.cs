using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Square : IShape
{
    private int[] _bound;

    public int[] Bounds { get => _bound; private set => _bound = value; }

    public Square(int bound)
    {
        Bounds = new int[] { bound };
    }

    public void DrawShape(Vector2 pos)
    {
        Graphics.DrawRectangle((int)pos.X, (int)pos.Y, Bounds[0], Bounds[0], Color.Black);
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

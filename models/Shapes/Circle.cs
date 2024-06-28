using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;


namespace AsteroidSharp.Models.Shapes;

class Circle : IShape
{
    private int[] _bounds;

    public int[] Bounds { get => _bounds; private set => _bounds = value; }

    public Circle(int bound)
    {
        Bounds = new int[] { bound };
    }

    public void DrawShape(Vector2 pos)
    {
        Graphics.DrawCircle((int)pos.X, (int)pos.Y, Bounds[0], Color.Black);
    }
}

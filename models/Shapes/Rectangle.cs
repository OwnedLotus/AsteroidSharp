using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models.Shapes;

class DrawRectangle : IShape
{
    public int[] Bounds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void DrawShape(int x, int y)
    {
        Graphics.DrawRectangle(x, y, Bounds[0], Bounds[1], Color.Black);
    }
}

using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models.Shapes;

class Square : IShape
{
    public Position Pos { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int[] Bounds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void DrawShape()
    {
        Graphics.DrawRectangle(Pos.X, Pos.Y, Bounds[0], Bounds[0], Color.Black);
    }
}

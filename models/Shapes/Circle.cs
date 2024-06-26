using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;


namespace AsteroidSharp.Models.Shapes;

class Circle : IShape
{
    public Position Pos { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int[] Bounds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void DrawShape()
    {
        Graphics.DrawCircle(Pos.X, Pos.Y, Bounds[0], Color.Black);
    }
}

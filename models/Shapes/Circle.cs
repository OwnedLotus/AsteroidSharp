using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;


namespace AsteroidSharp.Models.Shapes;

class Circle : IShape
{
    public Position pos { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float[] bounds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void DrawShape()
    {
        Graphics.DrawCircle(pos.X, pos.Y, bounds[0], Color.Black);
    }

    public float GetArea()
    {
        throw new NotImplementedException();
    }

    public int[] GetDimentsions()
    {
        throw new NotImplementedException();
    }
}

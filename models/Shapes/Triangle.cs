using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Triangle : IShape
{
    private (Vector2, Vector2, Vector2) local;

    private int[] _bounds;

    public int[] Bounds { get => _bounds; private set => _bounds = value; }
    public Triangle(int[] bounds)
    {
        Bounds = bounds;

        // this is the local offset of the three corners of the triangles
        //                  this is the top point               this is the bottom left point                   this is the bottom right point
        local = (new Vector2(0,(float)-_bounds[0]), new Vector2((float)-_bounds[1], bounds[0] / 2), new Vector2((float)_bounds[1], bounds[0] / 2));

    }



    public void DrawShape(Vector2 pos)
    {
        // the locals will be added to the absolute position to get the location for the points to be drawn
        var corner1 = local.Item1 + pos;
        var corner2 = local.Item2 + pos;
        var corner3 = local.Item3 + pos;




        Graphics.DrawTriangle(corner1, corner2, corner3, Color.Black);
    }
}

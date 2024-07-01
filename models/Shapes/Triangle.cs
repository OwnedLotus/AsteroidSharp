using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AsteroidSharp.Models.Shapes;

class Triangle : IShape
{
    private Vector2[] localCoordinates;

    private int[] _bounds;

    public int[] Bounds { get => _bounds; private set => _bounds = value; }
    public Triangle(int[] bounds)
    {
        Bounds = bounds;

        // this is the local offset of the three corners of the triangles
        //                  this is the top point               this is the bottom left point                   this is the bottom right point
        localCoordinates = new Vector2[3]
         {
            new Vector2(0,(float)-_bounds[0]),
            new Vector2((float)-_bounds[1], bounds[0] / 2),
            new Vector2((float)_bounds[1], bounds[0] / 2)
         };
    }

    public void DrawShape(Vector2 pos)
    {
        // the locals will be added to the absolute position to get the location for the points to be drawn
        var corner1 = localCoordinates[0] + pos;
        var corner2 = localCoordinates[1] + pos;
        var corner3 = localCoordinates[2] + pos;

        Graphics.DrawTriangle(corner1, corner2, corner3, Color.Black);
    }

    public void RotateShapeClockwise(Vector2 pos, float rotateSpeed)
    {
        throw new NotImplementedException();
    }

    public void RotateShapeCounterClockwise(Vector2 pos, float rotateSpeed)
    {
        for (int i = 0; i < localCoordinates.Length; i++)
        {
            // localCoordinates[i] = new Vector2(
            //     localCoordinates[i].X / MathF.Cos(rotateSpeed) - localCoordinates[i].Y / MathF.Sin(rotateSpeed),
            //     localCoordinates[i].X / MathF.Sin(rotateSpeed) - localCoordinates[i].Y / MathF.Cos(rotateSpeed)
            //     );
        }

        // May try to implement a quaternion

    }
}

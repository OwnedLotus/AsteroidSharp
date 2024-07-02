using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AsteroidSharp.Models.Shapes;

class Triangle : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;

    private int[] _bounds;

    public int[] Bounds { get => _bounds; private set => _bounds = value; }
    public Triangle(int[] bounds)
    {
        _bounds = new int[2];

        Bounds = bounds;
    
        // this is the local offset of the three corners of the triangles
        localCoordinates = new Vector2[3]
        {
            new Vector2(0,(float)-_bounds[0]),
            new Vector2((float)-_bounds[1], bounds[0] / 2),
            new Vector2((float)_bounds[1], bounds[0] / 2)
        };

        globalCoordinates = new Vector2[3];
    }

    public void DrawShape()
    {
        Graphics.DrawTriangle(globalCoordinates[0], globalCoordinates[1], globalCoordinates[2], Color.Black);
    }

    public void RotateShape(Vector2 pos, float degreesRotatedPerIteration)
    {
        float thetaRadians = MathF.PI * degreesRotatedPerIteration / 180;


        for (int i = 0; i < localCoordinates.Length; i++)
        {
            localCoordinates[i] = new Vector2(
                localCoordinates[i].X * MathF.Cos(thetaRadians) - localCoordinates[i].Y * MathF.Sin(thetaRadians),
                localCoordinates[i].X * MathF.Sin(thetaRadians) - localCoordinates[i].Y * MathF.Cos(thetaRadians)
                );
        }
    }

    public void UpdateShape(Vector2 pos)
    {
        // the locals will be added to the absolute position to get the location for the points to be drawn
        for (int i = 0; i < localCoordinates.Length; i++)
        {
            globalCoordinates[i] = localCoordinates[i] + pos;
        }

    }
}

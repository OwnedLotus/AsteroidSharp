using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AsteroidSharp.Models.Shapes;

class Triangle : IShape
{
    private Color _color = Color.White;
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;

    private int[] _bounds;

    public int[] Bounds { get => _bounds; private set => _bounds = value; }
    public Color Color { get => _color; set => _color = value; }

    public Triangle(int[] bounds, Vector2 orientation)
    {
        _bounds = new int[2];

        Bounds = bounds;
    
        // this is the local offset of the three corners of the triangles
        localCoordinates = new Vector2[3]
        {
            orientation * -_bounds[0],
            new Vector2((float)-_bounds[1], bounds[0] / 2),
            new Vector2((float)_bounds[1], bounds[0] / 2)
        };

        globalCoordinates = new Vector2[3];
    }

    public void DrawShape()
    {
        Graphics.DrawTriangle(globalCoordinates[0], globalCoordinates[1], globalCoordinates[2], _color);
    }

    public Vector2 RotateShape(Vector2 pos, float degreesRotatedPerIteration)
    {
        float thetaRadians = MathF.PI * degreesRotatedPerIteration / 180;

        var newCoords = new Vector2[3];

        // runs the formula of rotation for every coordinate in the shape
        for (int i = 0; i < localCoordinates.Length; i++)
        {
            newCoords[i] = new Vector2(
                localCoordinates[i].X * MathF.Cos(thetaRadians) - localCoordinates[i].Y * MathF.Sin(thetaRadians),
                localCoordinates[i].X * MathF.Sin(thetaRadians) + localCoordinates[i].Y * MathF.Cos(thetaRadians)
                );
        }

        localCoordinates = newCoords;
        
        //updated heading
        return Vector2.Normalize(localCoordinates[0]);
    }

    public Vector2 UpdateShape(Vector2 pos)
    {
        // the locals will be added to the absolute position to get the location for the points to be drawn
        for (int i = 0; i < localCoordinates.Length; i++)
        {
            globalCoordinates[i] = localCoordinates[i] + pos;
        }

        return Vector2.Normalize(localCoordinates[0]);
    }
}

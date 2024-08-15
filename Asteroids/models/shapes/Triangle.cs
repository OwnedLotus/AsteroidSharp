using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Triangle : IShape
{
    private const ushort numCorners = 3;

    private Color _color;
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;

    private Vector2 _bounds;

    public Vector2[] Corners { get => globalCoordinates; private set => globalCoordinates = value; }

    public Vector2 Bounds { get => _bounds; private set => _bounds = value; }
    public Color ShapeColor { get => _color; set => _color = value; }
    public ActorState State { get; set; }

    public Triangle(Vector2 bounds, Vector2 orientation, Color color)
    {
        Bounds = bounds;

        // this is the local offset of the three corners of the triangles
        localCoordinates = new Vector2[numCorners]
        {
            orientation * -_bounds.X,
            new Vector2(-_bounds.Y, bounds.X / 2),
            new Vector2(_bounds.Y, bounds.X / 2)
        };

        _color = color;
        globalCoordinates = new Vector2[numCorners];
    }

    public void DrawShape()
    {
        Graphics.DrawTriangleLines(globalCoordinates[0], globalCoordinates[1], globalCoordinates[2], _color);
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

    public bool Collision(IEnumerable<Vector2> points)
    {
        foreach (var point in points)
        {
            float sumAngle = 0;

            var diffToA = point - Corners[0];
            var diffToB = point - Corners[1];
            var diffToC = point - Corners[2];

            var thetaAB = MathF.Acos(Vector2.Dot(diffToA, diffToB) / (diffToA.Length() * diffToB.Length()));
            var thetaBC = MathF.Acos(Vector2.Dot(diffToB, diffToC) / (diffToB.Length() * diffToC.Length()));
            var thetaAC = MathF.Acos(Vector2.Dot(diffToA, diffToC) / (diffToA.Length() * diffToC.Length()));

            sumAngle = thetaAB + thetaAC + thetaBC;

            if (MathF.Abs(sumAngle - 2 * MathF.PI) < 1e-5)
                return true;

        }
        return false;
    }
}

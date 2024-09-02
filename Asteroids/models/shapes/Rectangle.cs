using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Rect : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;

    private Vector2 _bounds;
    private Color _color;

    public Vector2[] Corners { get => globalCoordinates; private set => globalCoordinates = value; }
    public Color ShapeColor { get => _color; private set => _color = value; }
    public ActorState State { get; set; }

    public Rect(Vector2 bounds, Color color)
    {
        _bounds = bounds;
        globalCoordinates = new Vector2[4];
        localCoordinates = new Vector2[4]
        {
            new Vector2(-_bounds.X / 2, -_bounds.Y / 2),
            new Vector2(_bounds.X / 2, -_bounds.Y / 2),
            new Vector2(-_bounds.X / 2, _bounds.Y / 2),
            new Vector2(_bounds.X / 2, _bounds.Y / 2),
        };
        ShapeColor = color;
    }


    public void DrawShape()
    {
        // ab
        Graphics.DrawLine((int)globalCoordinates[0].X, (int)globalCoordinates[0].Y, (int)globalCoordinates[1].X, (int)globalCoordinates[1].Y, _color);
        // ac
        Graphics.DrawLine((int)globalCoordinates[0].X, (int)globalCoordinates[0].Y, (int)globalCoordinates[2].X, (int)globalCoordinates[2].Y, _color);
        // bd
        Graphics.DrawLine((int)globalCoordinates[1].X, (int)globalCoordinates[1].Y, (int)globalCoordinates[3].X, (int)globalCoordinates[3].Y, _color);
        // cd
        Graphics.DrawLine((int)globalCoordinates[2].X, (int)globalCoordinates[2].Y, (int)globalCoordinates[3].X, (int)globalCoordinates[3].Y, _color);
    }

    public Vector2 RotateShape(Vector2 pos, float rotateSpeed)
    {
        float thetaRadians = MathF.PI * rotateSpeed / 180;

        var newCoords = new Vector2[4];

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

    public bool Collision(Vector2[] points)
    {
        foreach (var point in points)
        {
            float sumAngle = 0;

            var diffToA = point - Corners[0];
            var diffToB = point - Corners[1];
            var diffToC = point - Corners[2];
            var diffToD = point - Corners[3];

            var thetaAB = MathF.Acos(Vector2.Dot(diffToA, diffToB) / (diffToA.Length() * diffToB.Length()));
            var thetaBD = MathF.Acos(Vector2.Dot(diffToB, diffToD) / (diffToB.Length() * diffToD.Length()));
            var thetaAC = MathF.Acos(Vector2.Dot(diffToA, diffToC) / (diffToA.Length() * diffToC.Length()));
            var thetaCD = MathF.Acos(Vector2.Dot(diffToC, diffToD) / (diffToC.Length() * diffToD.Length()));

            sumAngle = thetaAB + thetaBD + thetaAC + thetaCD;

            if (MathF.Abs(sumAngle - 2 * MathF.PI) < 1e-5)
                return true;
        }

        return false;
    }
}

using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Rectangle : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;

    private Vector2 _bounds;
    private Color _color;

    public Vector2[] Corners { get => globalCoordinates; private set => globalCoordinates = value; }
    public Color ShapeColor { get => _color; private set => _color = value; }


    public Rectangle(Vector2 bounds, Color color)
    {
        _bounds = bounds;
        globalCoordinates = new Vector2[4];
        localCoordinates = new Vector2[4];
        ShapeColor = color;
    }


    public void DrawShape()
    {
        Graphics.DrawRectangleV(globalCoordinates[0], _bounds, ShapeColor);
    }

    public Vector2 RotateShape(Vector2 pos, float rotateSpeed)
    {
        float thetaRadians = MathF.PI * rotateSpeed / 180;

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

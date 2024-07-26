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
        Graphics.DrawLine((int)globalCoordinates[0].X, (int)globalCoordinates[0].Y, (int)globalCoordinates[1].X, (int)globalCoordinates[1].Y, _color);
        Graphics.DrawLine((int)globalCoordinates[0].X, (int)globalCoordinates[0].Y, (int)globalCoordinates[2].X, (int)globalCoordinates[2].Y, _color);
        Graphics.DrawLine((int)globalCoordinates[1].X, (int)globalCoordinates[1].Y, (int)globalCoordinates[3].X, (int)globalCoordinates[3].Y, _color);
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

    public bool Collision(IEnumerable<Vector2> boundaries)
    {
        foreach (var point in boundaries)
        {   
            // a b
            // c d
            // if one angle between points is larger than 180 degrees then it is not in the triangle
            Vector2 a_to_point = globalCoordinates[0] - point;
            Vector2 b_to_point = globalCoordinates[1] - point;
            Vector2 c_to_point = globalCoordinates[2] - point;
            Vector2 d_to_point = globalCoordinates[3] - point;

            double a_b_theta = Math.Acos(Vector2.Dot(a_to_point, b_to_point) / (a_to_point.Length() * b_to_point.Length()));
            double b_d_theta = Math.Acos(Vector2.Dot(b_to_point, d_to_point) / (b_to_point.Length() * d_to_point.Length()));
            double a_c_theta = Math.Acos(Vector2.Dot(a_to_point, c_to_point) / (a_to_point.Length() * c_to_point.Length()));
            double c_d_theta = Math.Acos(Vector2.Dot(c_to_point, d_to_point) / (c_to_point.Length() * d_to_point.Length()));

            if (a_b_theta <= Math.PI || b_d_theta <= Math.PI || a_c_theta <= Math.PI || c_d_theta <= Math.PI)
            {
                return true;
            }
        }
        return false;
    }
}
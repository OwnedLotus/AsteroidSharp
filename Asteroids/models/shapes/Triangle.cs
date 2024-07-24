using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Triangle : IShape
{
    private Color _color = Color.White;
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;

    private Vector2 _bounds;

    public Vector2[] Corners { get => globalCoordinates; private set => globalCoordinates = value; }

    public Vector2 Bounds { get => _bounds; private set => _bounds = value; }
    public Color ShapeColor { get => _color; set => _color = value; }

    public Triangle(Vector2 bounds, Vector2 orientation)
    {
        Bounds = bounds;

        // this is the local offset of the three corners of the triangles
        localCoordinates = new Vector2[3]
        {
            orientation * -_bounds.X,
            new Vector2(-_bounds.Y, bounds.X / 2),
            new Vector2(_bounds.Y, bounds.X / 2)
        };

        globalCoordinates = new Vector2[3];
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

    // Great candidate for unit testing !!!
    //TODO!
    public bool Collision(IEnumerable<Vector2> boundaries)
    {
        foreach (var point in boundaries)
        {   
            //  a
            // b c
            // if one angle between points is larger than 180 degrees then it is not in the triangle
            Vector2 a_to_point = globalCoordinates[0] - point;
            Vector2 b_to_point = globalCoordinates[1] - point;
            Vector2 c_to_point = globalCoordinates[2] - point;

            double a_b_theta = Math.Acos(Vector2.Dot(a_to_point,b_to_point) / (a_to_point.Length() * b_to_point.Length()));
            double b_c_theta = Math.Acos(Vector2.Dot(b_to_point,c_to_point) / (b_to_point.Length() * c_to_point.Length()));
            double a_c_theta = Math.Acos(Vector2.Dot(a_to_point,c_to_point) / (a_to_point.Length() * c_to_point.Length()));

            if (a_b_theta <= Math.PI || b_c_theta <= Math.PI || a_c_theta <= Math.PI)
            {
                return true;
            }
        }
        return false;
    }
}

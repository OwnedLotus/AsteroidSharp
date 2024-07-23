using System.Numerics;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models.Shapes;

class BulletShape : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;
    private Vector2 _direction;
    const ushort bulletSize = 5;

    public Vector2[] Corners { get => globalCoordinates; private set => globalCoordinates = value; }
    public Color ShapeColor { get; set; }

    public BulletShape(Vector2 pos, Vector2 direction, Color color)
    {
        globalCoordinates = new Vector2[bulletSize]
        {
            pos, pos, pos, pos, pos
        };
        _direction = direction;
        localCoordinates = new Vector2[0];
    }

    public void DrawShape()
    {
        Graphics.DrawLineV(globalCoordinates[0], globalCoordinates[1], Color.Red);
    }

    // bullets are not supposed to rotate in midair
    public Vector2 RotateShape(Vector2 pos, float rotateSpeed)
    {
        // float thetaRadians = MathF.PI * rotateSpeed / 180;

        // var newCoords = new Vector2[3];

        // // runs the formula of rotation for every coordinate in the shape
        // for (int i = 0; i < localCoordinates.Length; i++)
        // {
        //     newCoords[i] = new Vector2(
        //         localCoordinates[i].X * MathF.Cos(thetaRadians) - localCoordinates[i].Y * MathF.Sin(thetaRadians),
        //         localCoordinates[i].X * MathF.Sin(thetaRadians) + localCoordinates[i].Y * MathF.Cos(thetaRadians)
        //         );
        // }

        // localCoordinates = newCoords;

        // //updated heading
        return Vector2.Normalize(localCoordinates[0]);
    }


    // Good point to introduce unit testing
    public Vector2 UpdateShape(Vector2 pos)
    {
        for (int i = bulletSize; i > 0; i--)
        {
            globalCoordinates[1] = globalCoordinates[i - 1];
        }


        return globalCoordinates[0] = pos;
    }

    public bool Collision(IEnumerable<Vector2> points)
    {
        bool collided = false;

        for (int i = 0; i < points.Count() - 1; i++)
        {
            Vector2 point1 = points.ElementAt(i);
            Vector2 point2 = points.ElementAt(i + 1);

            Vector2 front = globalCoordinates[0];

            Vector2 segment = point2 - point1;
            Vector2 currToStart = front - point1;

            float cross = Vector3.Cross(new Vector3(segment.X, segment.Y, 0), new Vector3(currToStart.X, currToStart.Y, 0)).Z;

        }

        // if (Math.Abs(cross) < 1e-9)
        //     {
        //         // Check if point lies between original points
        //         if (Math.Abs(segment.X) >= Math.Abs(segment.Y))
        //         {
        //             if (segment.X > 0)
        //                 return point1.X <= currPoint.X && currPoint.X <= point2.X;
        //             else
        //                 return point2.X <= currPoint.X && currPoint.X <= point1.X;
        //         }
        //         else
        //         {
        //             if (segment.Y > 0)
        //                 return point1.Y <= currPoint.Y && currPoint.Y <= point2.Y;
        //             else
        //                 return point2.Y <= currPoint.Y && currPoint.Y <= point1.Y;
        //         }
        //     }



        return collided;
    }


}

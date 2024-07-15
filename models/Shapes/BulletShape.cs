using System.Numerics;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models.Shapes;

class BulletShape : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;
    private Vector2 _direction;

    public Vector2[] Corners { get => globalCoordinates; private set => globalCoordinates = value; }
    public Color ShapeColor { get; set; }

    public BulletShape( Vector2 pos, Vector2 direction, Color color)
    {
        globalCoordinates = new Vector2[2]
        {
            pos,
            new Vector2(pos.X * .75f, pos.Y * .75f)
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

    public Vector2 UpdateShape(Vector2 pos)
    {
        throw new NotImplementedException();
    }
}

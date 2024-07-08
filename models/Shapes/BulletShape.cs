using System.Numerics;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models.Shapes;

class BulletShape : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;
    private Vector2 _direction;
    private ushort length;
    private ushort totalLength;
    private Color _color;

    public Vector2[] Corners { get => globalCoordinates; private set => globalCoordinates = value; }
    public Color ShapeColor { get; set; }

    public BulletShape(ushort length, Vector2 direction, Color color)
    {
        localCoordinates = new Vector2[2];
        globalCoordinates = new Vector2[2];
        _direction = direction;
        length = 0;
        totalLength = length;
    }

    public void DrawShape()
    {
        Graphics.DrawLineV(globalCoordinates[0], globalCoordinates[1], Color.Red);
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
        throw new NotImplementedException();
    }
}

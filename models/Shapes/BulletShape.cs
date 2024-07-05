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

    public Vector2[] Corners { get => globalCoordinates; private set => globalCoordinates = value; }
    public Color Color { get; set; }

    public BulletShape(ushort length, Vector2 direction)
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
        throw new NotImplementedException();
    }

    public Vector2 UpdateShape(Vector2 pos)
    {
        throw new NotImplementedException();
    }
}

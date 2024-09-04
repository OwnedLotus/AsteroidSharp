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
    public ActorState State { get; set; }

    public BulletShape(Vector2 pos, Vector2 direction, Color color)
    {
        globalCoordinates = new Vector2[bulletSize]
        {
            pos, pos, pos, pos, pos
        };
        _direction = direction;
        localCoordinates = Array.Empty<Vector2>();
    }

    public void DrawShape()
    {
        Graphics.DrawLineV(globalCoordinates[0], globalCoordinates[1], Color.Red);
    }

    // bullets are not supposed to rotate in midair
    public Vector2 RotateShape(Vector2 pos, float rotateSpeed)
    {
        return Vector2.Normalize(localCoordinates[0]);
    }

    // Good point to introduce unit testing
    public Vector2 UpdateShape(Vector2 pos)
    {
        for (int i = bulletSize - 1; i >= 1; i--)
        {
            globalCoordinates[i] = globalCoordinates[i - 1];
        }

        return globalCoordinates[0] = pos;
    }

    public void ClearPos(Vector2 pos)
    {
        for (int i = 0; i < globalCoordinates.Count(); i++)
        {
            globalCoordinates[i] = pos;
        }
    }

    public bool Collision(Vector2[] points)
    {
        throw new NotImplementedException();
    }
}

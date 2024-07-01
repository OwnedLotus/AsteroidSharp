using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Bullet : IShape
{
    private int[] _bounds;

    public int[] Bounds { get => _bounds; private set => _bounds = value; }

    public void DrawShape(Vector2 pos)
    {
        throw new NotImplementedException();
    }

    public void RotateShapeClockwise(Vector2 pos, float rotateSpeed)
    {
        throw new NotImplementedException();
    }

    public void RotateShapeCounterClockwise(Vector2 pos, float rotateSpeed)
    {
        throw new NotImplementedException();
    }
}

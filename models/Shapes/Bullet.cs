using System.Numerics;

namespace AsteroidSharp.Models.Shapes;

class Bullet : IShape
{
    private Vector2[] localCoordinates;
    private Vector2[] globalCoordinates;
    
    private int[] _bounds;

    public int[] Bounds { get => _bounds; private set => _bounds = value; }

    public void DrawShape()
    {
        throw new NotImplementedException();
    }

    public void RotateShape(Vector2 pos, float rotateSpeed)
    {
        throw new NotImplementedException();
    }

    public void UpdateShape(Vector2 pos)
    {
        throw new NotImplementedException();
    }
}

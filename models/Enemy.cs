using System.Numerics;
using AsteroidSharp.Models.Shapes;


namespace AsteroidSharp.Models;

class Enemy(Vector2 StartingPosition, Vector2 localDirection, float s)
{
    private Vector2 _pos = StartingPosition;
    private Vector2 _heading = localDirection;
    private IShape _shape = new EnemyShape();
    private float _speed = s;


    public void DrawEnemy()
    {
        _shape.DrawShape();
    }

    public void MoveEnemy()
    {
        throw new NotImplementedException();
    }
    public void EnemyShoot()
    {
        throw new NotImplementedException();
    }
}

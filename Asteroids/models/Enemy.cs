using System.Numerics;
using AsteroidSharp.Models.Shapes;


namespace AsteroidSharp.Models;

class Enemy(Vector2 StartingPosition, Vector2 localDirection, float s)
{
    private Vector2 _pos = StartingPosition;
    private Vector2 _heading = localDirection;
    private IShape _shape = new EnemyShape();
    private float _speed = s;
    private Queue<Bullet> _bullets = new Queue<Bullet>();

    public void DrawEnemy()
    {
        _shape.DrawShape();
    }

    public void MoveEnemy()
    {
        _pos += _heading * _speed;
    }
    public void EnemyShoot()
    {
        _bullets.Enqueue(new Bullet());
    }
    public bool CheckCollision(IEnumerable<Vector2> boundries)
    {
        if (_shape is not null)
            return _shape.Collision(boundries);
        return false;
    }
}

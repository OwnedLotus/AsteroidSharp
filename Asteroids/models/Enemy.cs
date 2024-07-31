using System.Numerics;
using AsteroidSharp.Models.Shapes;
using Raylib_CSharp.Colors;


namespace AsteroidSharp.Models;

class Enemy(Vector2 StartingPosition, Vector2 localDirection, float s, Color color)
{
    private Vector2 _pos = StartingPosition;
    private Vector2 _heading = localDirection;
    private IShape _shape = new EnemyShape(new Vector2(10, 5), color, StartingPosition);
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

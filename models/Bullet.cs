using AsteroidSharp.Models.Shapes;
using System.Numerics;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models;

class Bullet(BulletShape bs, Vector2 pos, Vector2 heading, Color color, float s = 1)
{
    private BulletShape _shape = bs;
    private Vector2 _position = pos;
    private Vector2 _heading = heading;
    private Color _color = color;
    private float _bulletspeed = s;

    public void DrawBullet()
    {
        _shape.DrawShape();
    }

    public void Move()
    {
        _position += _heading * _bulletspeed;

        _shape.UpdateShape(_position);
    }

    // I have not decided how I want to check for collisions
    // Every idea that I have seems to me slow
    public bool CollisionCheck(Vector2 collisionPoint)
    {
        if (collisionPoint == _position)
            return true;
        else
            return false;
    }
}

using AsteroidSharp.Models.Shapes;
using System.Numerics;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models;

class Bullet(BulletShape bs, Vector2 pos, Vector2 heading, Color color)
{
    private BulletShape _shape = bs;
    private Vector2 _position = pos;
    private Vector2 _heading = heading;
    private Color _color = color;

    public void DrawBullet()
    {
        _shape.DrawShape();
    }

    public void Move()
    {
        throw new NotImplementedException();
    }

    // I have not decided how I want to check for collisions
    // Every idea that I have seems to me slow
    public void CollisionCheck()
    {
        throw new NotImplementedException();
    }


}

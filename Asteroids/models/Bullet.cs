using AsteroidSharp.Models.Shapes;
using System.Numerics;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models;

class Bullet(Vector2 pos, Vector2 heading, Color color, float speed, bool fromPlayer)
{
    private BulletShape _shape = new BulletShape(pos, heading, color);
    private Vector2 _position = pos;
    private Vector2 _heading = heading;
    private Color _color = color;
    private float _bulletspeed = speed;
    public Vector2 Position { get => _position; set => _position = value; }
    public bool FromPlayer = fromPlayer;
    public Vector2[] Corners { get => _shape.Corners; }
    public IShape Shape { get => _shape; }

    public void DrawBullet()
    {
        _shape.DrawShape();
    }

    public void Move(float deltaTime)
    {
        _position += _heading * _bulletspeed;
        _shape.UpdateShape(_position);
    }

    public bool CollisionCheck(IEnumerable<Vector2> boundaries)
    {
        return _shape.Collision(boundaries);
    }
}

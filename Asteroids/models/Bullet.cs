using AsteroidSharp.Models.Shapes;
using System.Numerics;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models;

class Bullet
{
    private BulletShape? _shape;
    private Vector2 _position;
    private Vector2 _heading;
    private Color _color;
    private float _bulletspeed;
    public Vector2 Position { get => _position; set => _position = value; }
    public bool FromPlayer;
    public Vector2[]? Corners { get => _shape?.Corners; }

    public void SpawnBullet(Vector2 pos, Vector2 heading, Color color, float speed, ushort length, bool fromPlayer)
    {
        _position = pos;
        _heading = heading;
        _color = color;
        _bulletspeed = speed;
        _shape = new BulletShape(_position, _heading, _color);
        FromPlayer = fromPlayer;
    }

    public void DrawBullet()
    {
        if (_shape is not null)
            _shape.DrawShape();
    }

    public void Move(float deltaTime)
    {
        if (_shape is not null)
        {
            _position += _heading * _bulletspeed;
            _shape.UpdateShape(_position);
        }
    }

    public bool CollisionCheck(IEnumerable<Vector2> boundries)
    {
        if (_shape is not null)
            return _shape.Collision(boundries);

        return false;
    }
}

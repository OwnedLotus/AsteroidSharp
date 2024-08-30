using AsteroidSharp.Models.Shapes;
using System.Numerics;
using Raylib_CSharp.Colors;

namespace AsteroidSharp.Models;

class Bullet(Vector2 pos, Vector2 heading, Color color, float speed, bool fromPlayer)
{
    private BulletShape _shape = new BulletShape(pos, heading, color);
    private Vector2 _position;
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
        DebugPrinter();
    }
    
    public void SpawnLocation(Vector2 pos, Vector2 heading)
    {
        _position = pos;
        _shape.ClearPos(pos);
        _heading = heading;
    }

    public bool CollisionCheck(Vector2[] boundaries)
    {
        return _shape.Collision(boundaries);
    }

    public void DebugPrinter()
    {

        string output = "Bullet Position List: ";
        foreach(var coord in this._shape.Corners)
        {
            output += coord.ToString() + ", ";
        }
        // Console.WriteLine(output);
    }
}

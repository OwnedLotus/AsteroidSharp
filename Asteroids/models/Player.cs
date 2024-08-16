using System.Diagnostics;
using System.Numerics;

using AsteroidSharp.Models.Shapes;

using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;

namespace AsteroidSharp.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
class Player
{
    // oriented up
    private Color _color = Color.White;
    private Vector2 _heading;
    private Vector2 _position;
    private float _momentum;
    // private Vector2 _previousPosition;
    private Queue<Bullet> bullets;
    private uint numberOfBullets = 5;
    private Triangle _shape;
    private (uint, uint) windowDimensions;
    private float coefficientOfFriction;

    public Vector2[] Corners { get => _shape.Corners; }
    public float RotationAngle { get; private set; }
    public float Speed { get; private set; }

    public List<Bullet> activeBullets;

    public Vector2 Position { get => _position; }
    public Vector2 Heading { get => _heading; private set => _heading = Vector2.Normalize(value); }
    public ActorState State { get => _shape.State; }

    public Player(Vector2 pos, Vector2 vel, (uint, uint) dimensions, float cof = 1, float s = 2, float r = 5, float m = 1)
    {
        _position = pos;
        RotationAngle = r;
        Speed = s;
        coefficientOfFriction = cof;
        _shape = new Triangle(new Vector2(10, 5), Vector2.UnitY, _color);
        _momentum = m;
        bullets = new();
        activeBullets = new();
        _heading = -Vector2.UnitY;
        windowDimensions = dimensions;
        _shape.State = ActorState.Active;

    }

    #region Private Methods

    private void Shoot()
    {
        if (bullets.Count + activeBullets.Count < numberOfBullets)
        {
            bullets.Enqueue(new(_position, _heading, Color.Red, 10, true));
        }

        var success = bullets.TryDequeue(out Bullet? bullet);

        if (success && bullet is not null)
        {

            bullet.SpawnLocation(_position, _heading);
            activeBullets.Add(bullet);
        }
    }

    private void TeleportPlayerUp() => _position.Y = 0;
    private void TeleportPlayerLeft() => _position.X = 0;
    private void TeleportPlayerDown() => _position.Y = windowDimensions.Item2;
    private void TeleportPlayerRight() => _position.X = windowDimensions.Item1;

    private string GetDebuggerDisplay()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Public Methods
    public void DrawPlayer()
    {
        _shape.DrawShape();

        foreach (var bullet in activeBullets)
        {
            bullet.DrawBullet();
        }
    }

    public void UpdatePlayer(float deltaTime)
    {
        _heading = _shape.UpdateShape(_position);
        var newPos = _position + _heading * Speed;

        if (Input.IsKeyDown(KeyboardKey.W))
        {
            _position = newPos;
            _momentum = Speed;
        }

        
        {
            // outside the borders of the game
            
        }

        // rotation
        if (Input.IsKeyDown(KeyboardKey.A)) _shape.RotateShape(_position, -RotationAngle);
        if (Input.IsKeyDown(KeyboardKey.D)) _shape.RotateShape(_position, RotationAngle);
        if (Input.IsKeyDown(KeyboardKey.Space)) Shoot();

        if (_position.Y < 0) TeleportPlayerDown();
        if (_position.Y > windowDimensions.Item2) TeleportPlayerUp();
        if (_position.X < 0) TeleportPlayerRight();
        if (_position.X > windowDimensions.Item1) TeleportPlayerLeft();
    }

    public void DespawnBullet(Bullet bullet)
    {
        bullets.Enqueue(bullet);
        activeBullets.Remove(bullet);
    }

    public bool CheckCollisions(IEnumerable<Vector2> boundaries)
    {
        if (_shape.Collision(boundaries))
        {
            _shape.State = ActorState.Destroyed;
            return true;
        }

        return false;
    }

    #endregion
}

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
    private Vector2 _heading;
    private Vector2 _position;
    private float _momentum;
    // private Vector2 _previousPosition;
    private Queue<Bullet> bullets;
    private uint numberOfBullets = 100;
    private Triangle _shape;
    private (uint, uint) windowDimensions;
    private float coefficientOfFriction;

    public Vector2[] Corners { get => _shape.Corners; }
    public float RotationAngle { get; private set; }
    public float Speed { get; private set; }

    public List<Bullet> activeBullets;

    public Vector2 Position { get => _position; }
    public Vector2 Heading { get => _heading; private set => _heading = Vector2.Normalize(value); }

    public Player(Vector2 pos, Vector2 vel, (uint, uint) dimensions, float cof = 1, float s = 2, float r = 5, float m = 1)
    {
        _position = pos;
        RotationAngle = r;
        Speed = s;
        coefficientOfFriction = cof;
        _shape = new Triangle(new Vector2(10, 5), Vector2.UnitY);
        _momentum = m;
        bullets = new();
        activeBullets = new();
        _heading = -Vector2.UnitY;
        windowDimensions = dimensions;

        for (int i = 0; i < numberOfBullets; i++)
        {
            bullets.Enqueue(new Bullet());
        }
    }

    #region Private Methods

    private void Shoot()
    {
        var success = bullets.TryDequeue(out Bullet? bullet);

        if (success && bullet is not null)
        {
            bullet.SpawnBullet(_position, _heading, Color.Red, 10, 5, true);
            activeBullets.Add(bullet!);
        }
    }

    private void DespawnBullet(Bullet bullet)
    {
        bullets.Enqueue(bullet);
        activeBullets.Remove(bullet);
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

        for (int i = 0; i < activeBullets.Count; i++)
        {
            // outside the borders of the game
            if (activeBullets[i].Position.X < 0 ||
                    activeBullets[i].Position.Y < 0 ||
                    activeBullets[i].Position.X > windowDimensions.Item1 ||
                    activeBullets[i].Position.Y > windowDimensions.Item2)
                DespawnBullet(activeBullets[i]);
            else
                activeBullets[i].Move(deltaTime);
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

    public bool CheckCollisions(IEnumerable<Vector2> boundries)
    {
        if (_shape is not null)
            return _shape.Collision(boundries);
        return false;
    }

    #endregion
}

using System.Diagnostics;
using Raylib_CSharp.Colors;
using System.Numerics;
using AsteroidSharp.Models.Shapes;
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
    private Queue<Bullet> activeBullets;
    private uint numberOfBullets = 5;


    public float RotationAngle { get; private set; }
    public float Speed { get; private set; }

    private float coefficientOfFriction;
    public Vector2 Position { get => _position; }
    public Vector2 Heading { get => _heading; private set => _heading = Vector2.Normalize(value); }
    private Triangle playerShape;

    public Player(Vector2 pos, Vector2 vel, float cof = 1, float s = 2, float r = 5, float m = 1)
    {
        _position = pos;
        RotationAngle = r;
        Speed = s;
        coefficientOfFriction = cof;
        playerShape = new Triangle(new Vector2(10, 5), Vector2.UnitY);
        _momentum = m;
        bullets = new Queue<Bullet>();
        activeBullets = new Queue<Bullet>();

        for (int i = 0; i < numberOfBullets; i++)
        {
            bullets.Enqueue(new Bullet());
        }
    }

    #region Private Methods

    private void Shoot()
    {
        Bullet shotBullet;

        var success = bullets.TryDequeue(out shotBullet!);

        if (success && shotBullet is not null)
        {
            shotBullet.Position = _position;
            activeBullets.Enqueue(shotBullet);
        }
    }

    private void DespawnBullet()
    {
        bullets.Enqueue(activeBullets.Dequeue());
    }

    private void TeleportPlayerUp() => _position.Y = 0;
    private void TeleportPlayerLeft() => _position.X = 0;
    private void TeleportPlayerDown(uint worldHeight) => _position.Y = worldHeight;
    private void TeleportPlayerRight(uint worldLength) => _position.X = worldLength;

    private string GetDebuggerDisplay()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Public Methods
    public void DrawPlayer()
    {
        playerShape.DrawShape();

        foreach (var bullet in activeBullets)
        {
            bullet.DrawBullet();
        }
    }

    public void UpdatePlayer((uint, uint) dimensions, float deltaTime)
    {
        _heading = playerShape.UpdateShape(_position);
        var newPos = _position + _heading * Speed * deltaTime;

        if (Input.IsKeyDown(KeyboardKey.W))
        {
            _position = newPos;
            _momentum = Speed;
        }

        // rotation
        if (Input.IsKeyDown(KeyboardKey.A)) playerShape.RotateShape(_position, -RotationAngle);
        if (Input.IsKeyDown(KeyboardKey.D)) playerShape.RotateShape(_position, RotationAngle);

        if (Input.IsKeyDown(KeyboardKey.Space)) Shoot();


        if (_position.Y < 0) TeleportPlayerDown(dimensions.Item2);
        if (_position.Y > dimensions.Item2) TeleportPlayerUp();
        if (_position.X < 0) TeleportPlayerRight(dimensions.Item1);
        if (_position.X > dimensions.Item1) TeleportPlayerLeft();
    }

    #endregion
}

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
    private uint numberOfBullets = 30;

    public float RotationAngle { get; private set; }
    public float Speed { get; private set; }

    private float coefficientOfFriction;
    public Vector2 Position { get => _position; }
    public Vector2 Heading { get => _heading; private set => _heading = Vector2.Normalize(value); }
    private Triangle playerShape;
    private (uint, uint) windowDimensions;

    public Player(Vector2 pos, Vector2 vel, (uint, uint) dimensions, float cof = 1, float s = 2, float r = 5, float m = 1)
    {
        _position = pos;
        RotationAngle = r;
        Speed = s;
        coefficientOfFriction = cof;
        playerShape = new Triangle(new Vector2(10, 5), Vector2.UnitY);
        _momentum = m;
        bullets = new Queue<Bullet>();
        _heading = -Vector2.UnitY;
        windowDimensions = dimensions;

        for (int i = 0; i < numberOfBullets; i++)
        {
            bullets.Enqueue(new Bullet());
        }
    }

    #region Private Methods



    public Bullet? Shoot()
    {
        var success = bullets.TryDequeue(out Bullet? bullet);

        if (success)
            bullet?.SpawnBullet(_position, _heading, Color.Red, 10, 5);

        return bullet;
    }

    public void DespawnBullet(Bullet bullet)
    {
        bullets.Enqueue(bullet);
        bullet.IsActive = false;
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
        playerShape.DrawShape();
    }

    public void CheckBullet(ref List<Bullet> bullets, float deltaTime)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            // outside the borders of the game
            if (bullets[i].Position.X < 0 ||
                    bullets[i].Position.Y < 0 ||
                    bullets[i].Position.X > windowDimensions.Item1 ||
                    bullets[i].Position.Y > windowDimensions.Item2)
                DespawnBullet(bullets[i]);
            else
                bullets[i].Move(deltaTime);
        }
    }

    public void UpdatePlayer(float deltaTime)
    {
        _heading = playerShape.UpdateShape(_position);
        var newPos = _position + _heading * Speed;
        
        if (Input.IsKeyDown(KeyboardKey.W))
        {
            _position = newPos;
            _momentum = Speed;
        }

        // rotation
        if (Input.IsKeyDown(KeyboardKey.A)) playerShape.RotateShape(_position, -RotationAngle);
        if (Input.IsKeyDown(KeyboardKey.D)) playerShape.RotateShape(_position, RotationAngle);


        if (_position.Y < 0) TeleportPlayerDown();
        if (_position.Y > windowDimensions.Item2) TeleportPlayerUp();
        if (_position.X < 0) TeleportPlayerRight();
        if (_position.X > windowDimensions.Item1) TeleportPlayerLeft();
    }

    #endregion
}

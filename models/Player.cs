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
    private ushort bullets;
    private BulletShape bulletShape;
    private float _momentum;


    public float RotationAngle { get; private set; }
    public float Speed { get; private set; }

    private float coefficientOfFriction;
    public Vector2 Position { get => _position; }
    public Vector2 Heading { get => _heading; private set => _heading = Vector2.Normalize(value); }
    private Triangle playerShape;

    public Player(Vector2 pos, Vector2 vel, float cof = 1, float s = 2, ushort b = 5, float r = 5, float m = 1)
    {
        _position = pos;
        bullets = b;
        //bulletShape = new BulletShape(3);
        RotationAngle = r;
        Speed = s;
        coefficientOfFriction = cof;
        playerShape = new Triangle(new Vector2(10, 5), Vector2.UnitY);
        bulletShape = new BulletShape(5, Vector2.UnitY, Color.Red);
        _momentum = m;

    }

    #region Private Methods

    private void Shoot()
    {
        bullets--;
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
    }

    public void UpdatePlayer((uint, uint) dimensions)
    {
        _heading = playerShape.UpdateShape(_position);

        if (Input.IsKeyDown(KeyboardKey.W))
        {
            _heading *= 5f;
            _position += _heading * Speed;
        }

        // dampening forwards momentum
        if (Input.IsKeyDown(KeyboardKey.S) && _heading.Length() > 1f) _heading *= 0.9f;

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

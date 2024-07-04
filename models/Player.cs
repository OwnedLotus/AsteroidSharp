using System.Diagnostics;
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


    public float RotationAngle { get; private set; }
    public float Speed { get; private set; }
    private float coefficientOfFriction;
    public Vector2 Position { get => _position; }
    public Vector2 Heading { get => _heading; }
    private Triangle playerShape;

    public Player(Vector2 pos, Vector2 vel, float cof = 1, float s = 2, ushort b = 5, float r = 5)
    {
        _position = pos;
        bullets = b;
        bulletShape = new BulletShape(3);
        RotationAngle = r;
        Speed = s;
        coefficientOfFriction = cof;
        playerShape = new Triangle(new int[] { 10, 5 }, Vector2.UnitY);
    }



    #region Private Methods

    private void Shoot()
    {
        bullets--;
    }

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

    public void UpdatePlayer()
    {
        _heading = playerShape.UpdateShape(_position);

        if (Input.IsKeyDown(KeyboardKey.W)) _position += _heading * Speed;
        // System.Console.WriteLine(_position);
        // System.Console.WriteLine(_heading);

        // dampening forwards momentum

        // rotation
        if (Input.IsKeyDown(KeyboardKey.A)) playerShape.RotateShape(_position, -RotationAngle);
        if (Input.IsKeyDown(KeyboardKey.D)) playerShape.RotateShape(_position, RotationAngle);

        if (Input.IsKeyDown(KeyboardKey.Space)) Shoot();

    }

    public void TeleportPlayerUp()
    {
        _position.Y = 0;
    }
    public void TeleportPlayerLeft()
    {
        _position.X = 0;
    }

    public void TeleportPlayerDown(uint worldHeight)
    {
        _position.Y = worldHeight;
    }

    public void TeleportPlayerRight(uint worldLength)
    {
        _position.X = worldLength;
    }


    #endregion
}

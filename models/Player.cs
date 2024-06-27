using System.Diagnostics;
using System.Numerics;
using AsteroidSharp.Models.Shapes;
using Raylib_CSharp.Interact;

namespace AsteroidSharp.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
class Player(Vector2 pos, Vector2 vel,IShape shape, float cof = 1, float s = 1, ushort b = 5)
{
    private Vector2 position = pos;
    private ushort bullets = b;
    private Bullet bulletShape = new Bullet();


    public float Speed { get; private set; } = s;
    private float coefficientOfFriction = cof;
    public Vector2 Position { get => position; }
    public Vector2 NormalizedVelocity { get; private set; } = Vector2.Normalize(vel);

    private IShape playerShape = shape;

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
        playerShape.DrawShape((int)Position.X, (int)Position.Y);
    }

    public void MovePlayer()
    {
        if (Input.IsKeyDown(KeyboardKey.W)) position.Y -= Speed;
        if (Input.IsKeyDown(KeyboardKey.S)) position.Y += Speed;
        if (Input.IsKeyDown(KeyboardKey.A)) position.X -= Speed;
        if (Input.IsKeyDown(KeyboardKey.D)) position.X += Speed;

        if (Input.IsKeyDown(KeyboardKey.Space)) Shoot();
    }

    public void TeleportPlayerUp()
    {
        position.Y = 0;        
    }
    public void TeleportPlayerLeft()
    {
        position.X = 0;
    }

    public void TeleportPlayerDown(uint worldHeight)
    {
        position.Y = worldHeight;
    }

    public void TeleportPlayerRight(uint worldLength)
    {
        position.X = worldLength;
    }


#endregion
}
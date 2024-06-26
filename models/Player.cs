using System.Diagnostics;
using System.Numerics;
using Raylib_CSharp.Interact;

namespace AsteroidSharp.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
class Player(Position pos, Vector2 vel, float cof = 1)
{
    private float coefficientOfFriction = cof;
    public Position Pos { get; private set; } = pos;
    public Vector2 Velocity { get; private set; } = vel;

#region Private Methods

    private void Shoot()
    {
        throw new NotImplementedException();
    }

    private void DrawShot()
    {
        throw new NotImplementedException();
    }
    private string GetDebuggerDisplay()
    {
        throw new NotImplementedException();
    }

#endregion

#region Public Methods
    public void DrawPlayer()
    {
        throw new NotImplementedException();
    }

    public void MovePlayer(KeyboardKey key)
    {
        throw new NotImplementedException();
    }

#endregion
}
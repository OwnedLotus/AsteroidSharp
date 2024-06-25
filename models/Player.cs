using System.Diagnostics;
using System.Numerics;

namespace AsteroidSharp.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
class Player(Position pos, Vector2 vel, float cof = 1)
{
    private float coefficientOfFriction = cof;
    public Position Pos { get; private set; } = pos;
    public Vector2 Velocity { get; private set; } = vel;

    private string GetDebuggerDisplay()
    {
        throw new NotImplementedException();
    }
}
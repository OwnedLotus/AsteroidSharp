using System.Diagnostics;
using System.Numerics;

namespace AsteroidSharp.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class Asteroid(IShape s, Position p)
{
    public IShape Shape { get; private set; } = s;
    public Position Pos { get; private set; } = p;

    private string GetDebuggerDisplay()
    {
        throw new NotImplementedException();
    }
}
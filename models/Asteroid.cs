using System.Diagnostics;
using System.Numerics;

namespace AsteroidSharp.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class Asteroid(IShape shape, float speed)
{
    private float speed = speed;

    public IShape Shape { get; private set; } = shape;
    public Vector2 Pos { get; private set; }
    public Vector2 NormalizedVelocity { get; private set; }


    #region Private Methods

    private void SpawnAsteroid(uint xLength, uint yLength)
    {
        Random rng = new();

        // !TODO the idea is to use the 0 -> 1 float to scale how far along the axis the astroid is to spawn
        var xScaler = rng.NextSingle();
        var yScaler = rng.NextSingle();
    }

    private string GetDebuggerDisplay()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Public Methods

    public void Move()
    {
        Pos += NormalizedVelocity * speed;
    }

    public void DrawAsteroid()
    {
        Shape.DrawShape();
    }

    #endregion

}

using System.Diagnostics;
using System.Numerics;

namespace AsteroidSharp.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class Asteroid(IShape shape, float s)
{
    private float _speed = s;
    private int _rotationAngle;
    private IShape _shape = shape;

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

    private void CheckCollisions()
    {
    }


    private string GetDebuggerDisplay()
    {
        throw new NotImplementedException();
    }

    private void RotateAsteroid()
    {
        _shape.RotateShape(Pos, _rotationAngle);
    }

    #endregion

    #region Public Methods

    public void Move()
    {
        Pos += NormalizedVelocity * _speed;
        RotateAsteroid();
    }

    public void DrawAsteroid()
    {
        _shape.DrawShape();
    }

    #endregion

}

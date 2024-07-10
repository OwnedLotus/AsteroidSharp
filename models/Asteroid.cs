using System.Diagnostics;
using Raylib_CSharp.Colors;
using System.Numerics;
using System.Diagnostics.Contracts;

namespace AsteroidSharp.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class Asteroid
{
    private float _speed;
    private int _rotationAngle;
    private IShape? _shape;
    private Vector2? _position;

    public Vector2 position { get; private set ; }
    public Vector2 NormalizedVelocity { get; private set; }

    public Asteroid(uint xLength, uint  yLength, Vector2? pos = null)
    {
        Random rng = new();

        // !TODO the idea is to use the 0 -> 1 float to scale how far along the axis the astroid is to spawn
        var xScaler = rng.NextSingle();
        var yScaler = rng.NextSingle();

        _speed = rng.Next(0, 10);
        int shapeRng = rng.Next(0, 4);

        switch (shapeRng)
        {
            case 0:
                _shape = new Shapes.Circle(5);
                break;
            case 1:
                _shape = new Shapes.Square(5, Color.Brown);
                break;
            case 2:
                _shape = new Shapes.Triangle(new Vector2(10,5), Vector2.UnitY);
                break;
            case 3:
                _shape = new Shapes.Rectangle(new Vector2(10,5), Color.Brown);
                break;
        }

        if (pos is not null)
        {
            _position = pos;
        }
        else
        {
            
        }
    }


    #region Private Methods

    private void CheckCollisions()
    {
    }


    private string GetDebuggerDisplay()
    {
        throw new NotImplementedException();
    }

    private void RotateAsteroid()
    {
        if (_position is not null)
            _shape?.RotateShape((Vector2)_position, _rotationAngle);
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
        _shape?.DrawShape();
    }

    #endregion

}

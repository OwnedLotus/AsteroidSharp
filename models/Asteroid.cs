using System.Diagnostics;
using Raylib_CSharp.Colors;
using System.Numerics;

namespace AsteroidSharp.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class Asteroid
{
    private float _speed;
    private float _rotationAngle;
    private IShape? _shape;
    private Vector2 _position;
    private Vector2 _heading;

    public Vector2 position { get => _position; private set => _position = value; }
    public Vector2 Heading { get => _heading; private set => _heading = value; }

    public Asteroid((uint, uint) dimensions, float rotate = 10f, float s = 2)
    {
        Random rng = new();

        // !TODO the idea is to use the 0 -> 1 float to scale how far along the axis the astroid is to spawn
        var xScaler = rng.NextSingle();
        var yScaler = rng.NextSingle();

        _position = new Vector2(xScaler, yScaler);
        _heading = Vector2.UnitY;

        _speed = rng.Next(0, 10);
        int shapeRng = rng.Next(0, 4);
        // TODO! remove after debug movement
        shapeRng = 3;

        switch (shapeRng)
        {
            case 0:
                _shape = new Shapes.Circle(5);
                break;
            case 1:
                _shape = new Shapes.Square(5, Color.Brown);
                break;
            case 2:
                _shape = new Shapes.Triangle(new Vector2(10, 5), Vector2.UnitY);
                break;
            case 3:
                _shape = new Shapes.Rectangle(new Vector2(10, 5), Color.Brown);
                break;
        }

        if (_shape is not null)
            _shape.UpdateShape((Vector2)_position);

        _rotationAngle = rotate;
        _speed = s;
    }

    #region Private Methods

    private string GetDebuggerDisplay()
    {
        throw new NotImplementedException();
    }

    private void RotateAsteroid()
    {
        _shape?.RotateShape((Vector2)_position, _rotationAngle);
    }

    #endregion

    #region Public Methods

    public void Move(float deltaTime)
    {
        _position += Heading * _speed * deltaTime;
        RotateAsteroid();

        if (_shape is not null)
            _shape.UpdateShape((Vector2)_position);
    }

    public void DrawAsteroid()
    {
        _shape?.DrawShape();
    }

    #endregion

}

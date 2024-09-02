using System.Diagnostics;
using Raylib_CSharp.Colors;
using System.Numerics;
using AsteroidSharp.Models.Shapes;

namespace AsteroidSharp.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class Asteroid
{
    private float _speed;
    private float _rotationAngle;
    private IShape? _shape;
    private Vector2 _position;
    private Vector2 _heading = -Vector2.UnitY;
    private Color _color = Color.Brown;
    private int _scale;

    public Vector2 position { get => _position; private set => _position = value; }
    public Vector2 Heading { get => _heading; private set => _heading = value; }
    public IShape? Shape { get => _shape; }
    public ActorState State { get; set; }

    public Asteroid()
    {
    }

    public Asteroid((uint, uint) dimensions, int scale, float rotate = 5f, float s = 1.5f)
    {
        Random rng = new();

        // !TODO the idea is to use the 0 -> 1 float to scale how far along the axis the astroid is to spawn
        var xScaler = rng.NextSingle();
        var yScaler = rng.NextSingle();
        _scale = scale;

        _position = new Vector2(dimensions.Item1 / 2, dimensions.Item2 / 2);

        _speed = rng.Next(0, 10);
        int shapeRng = rng.Next(0, 4);

        switch (shapeRng)
        {
            case 0:
                _shape = new Shapes.Circle(_scale, _color);
                Console.WriteLine("Circle");
                break;
            case 1:
                _shape = new Shapes.Square(_scale, _color);
                Console.WriteLine("Square");
                break;
            case 2:
                _shape = new Shapes.Triangle(new Vector2(_scale, _scale / 2), Vector2.UnitY, _color);
                Console.WriteLine("Triangle");
                break;
            case 3:
                _shape = new Shapes.Rect(new Vector2(_scale, _scale / 2), _color);
                Console.WriteLine("Rectangle");
                break;
        }

        _shape!.UpdateShape(_position);

        _rotationAngle = rotate;
        _speed = s;

        _heading = new Vector2(rng.NextSingle(), rng.NextSingle());
    }

    #region Private Methods

    private string GetDebuggerDisplay()
    {
        throw new NotImplementedException();
    }

    private void RotateAsteroid()
    {
        _shape?.RotateShape(_position, _rotationAngle);
    }

    #endregion

    #region Public Methods

    public void Move(float deltaTime)
    {
        _position += Heading * _speed;
        Console.WriteLine("Updated Position: " + _position);
        RotateAsteroid();

        _shape?.UpdateShape(_position);
    }

    public void DrawAsteroid()
    {
        _shape?.DrawShape();
    }

    public bool CheckCollisions(Vector2[] boundaries)
    {
        if (_shape!.Collision(boundaries))
        {
            _shape.State = ActorState.Destroyed;
            return true;
        }

        return false;
    }

    public static Asteroid DebugCircleAsteroidSpawner()
    {
        var circleAsteroid = new Asteroid()
        {
            _position = Vector2.Zero,
            _shape = new Circle(5, Color.Brown)
        };

        return circleAsteroid;
    }

    public static Asteroid DebugSquareAsteroidSpawner()
    {
        var squareAsteroid = new Asteroid()
        {
            _position = Vector2.Zero,
            _shape = new Square(5, Color.Brown)
        };

        return squareAsteroid;
    }

    public static Asteroid DebugRectangleAsteroidSpawner()
    {
        var rectangleAsteroid = new Asteroid()
        {
            _position = Vector2.Zero,
            _shape = new Rect(new Vector2(5f, 5f), Color.Brown)
        };

        return rectangleAsteroid;
    }

    public static Asteroid DebugTriangleAsteroidSpawner()
    {

        var triangleAsteroid = new Asteroid()
        {
            _position = Vector2.Zero,
            _shape = new Triangle(new Vector2(5f, 5f), Vector2.UnitY, Color.Brown)
        };

        return triangleAsteroid;
    }

    #endregion

}

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
    private IShape _shape;
    private Vector2 _position;
    private Vector2 _heading;
    private Color _color = Color.Brown;

    public Vector2 position { get => _position; private set => _position = value; }
    public Vector2 Heading { get => _heading; private set => _heading = value; }
    public IShape Shape { get => _shape; }
    public ActorState State { get => _shape!.State; }

    public Asteroid()
    {
        _shape = new Shapes.Circle(0,Color.Black);
    }

    public Asteroid((uint, uint) dimensions, float rotate = 10f, float s = 2)
    {
        Random rng = new();

        // !TODO the idea is to use the 0 -> 1 float to scale how far along the axis the astroid is to spawn
        var xScaler = rng.NextSingle();
        var yScaler = rng.NextSingle();

        _position = new Vector2(dimensions.Item1 / 2, dimensions.Item2 / 2);
        _heading = Vector2.Zero;

        _speed = rng.Next(0, 10);
        int shapeRng = rng.Next(0, 4);

        shapeRng = 1;

        switch (shapeRng)
        {
            case 0:
                _shape = new Shapes.Circle(10, _color);
                break;
            case 1:
                _shape = new Shapes.Square(10, _color);
                break;
            case 2:
                _shape = new Shapes.Triangle(new Vector2(10, 5), Vector2.UnitY, _color);
                break;
            case 3:
                _shape = new Shapes.Rect(new Vector2(10, 5), _color);
                break;
        }

        if (_shape is not null)
            _shape.UpdateShape(_position);
        else
            _shape = new Shapes.Circle(0,Color.Black);

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
        _shape?.RotateShape(_position, _rotationAngle);
    }

    #endregion

    #region Public Methods

    public void Move(float deltaTime)
    {
        _position += Heading * _speed * deltaTime;
        RotateAsteroid();

        if (_shape is not null)
            _shape.UpdateShape(_position);
    }

    public void DrawAsteroid()
    {
        _shape.DrawShape();
    }

    public bool CheckCollisions(IEnumerable<Vector2> boundaries)
    {
        return _shape.Collision(boundaries);
    }

    public static Asteroid DebugCircleAsteroidSpawner()
    {
        var circleAsteroid = new Asteroid() {
            _position = Vector2.Zero,
            _shape = new Circle(5, Color.Brown)
        };

        return circleAsteroid;
    }

    public static Asteroid DebugSquareAsteroidSpawner()
    {
        var squareAsteroid = new Asteroid() {
            _position = Vector2.Zero,
            _shape = new Square(5, Color.Brown)
        };
        
        return squareAsteroid;
    }

    public static Asteroid DebugRectangleAsteroidSpawner()
    {
        var rectangleAsteroid = new Asteroid() {
            _position = Vector2.Zero,
            _shape = new Rect(new Vector2(5f,5f), Color.Brown)
        };

        return rectangleAsteroid;
    }

    public static Asteroid DebugTriangleAsteroidSpawner()
    {
    
        var triangleAsteroid = new Asteroid() {
            _position = Vector2.Zero,
            _shape = new Circle(5, Color.Brown)
        };

        return triangleAsteroid;
    }

    #endregion

}

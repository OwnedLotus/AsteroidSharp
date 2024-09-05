using System.Diagnostics;

using Raylib_CSharp.Colors;
using System.Numerics;
using AsteroidSharp.Models.Shapes;

namespace AsteroidSharp.Models;

public enum AsteroidState
{
    Large,
    Small,
    Destroyed
}

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
    private AsteroidState _asteroidState = AsteroidState.Large;

    public Vector2 Position { get => _position; private set => _position = value; }
    public Vector2 Heading { get => _heading; private set => _heading = value; }
    public IShape? Shape { get => _shape; }
    public int Scale { get => _scale; }
    public float Speed { get => _speed; }
    public AsteroidState State { get => _asteroidState; }

    public Asteroid() { }

    // intended to be called when breaking apart a large asteroid

    public Asteroid((int, int) dimensions, Vector2 pos, float rotate = 10f)
    {
        Random rng = new();

        // !TODO the idea is to use the 0 -> 1 float to scale how far along the axis the astroid is to spawn
        var xScaler = rng.NextSingle();
        var yScaler = rng.NextSingle();
        _scale = rng.Next(10, 100);

        _rotationAngle = rotate / _scale;
        _speed = rng.Next(1, 10);

        if (pos == Vector2.Zero)
        {
            _position = new Vector2(dimensions.Item1 * xScaler, dimensions.Item2 * yScaler);
            _asteroidState = AsteroidState.Large;
        }
        else
        {
            _position = pos;
            _asteroidState = AsteroidState.Small;
            _scale /= 2;
            _speed *= 2;
        }

        int shapeRng = rng.Next(0, 4);

        switch (shapeRng)
        {
            case 0:
                _shape = new Circle(_scale, _color);
                break;
            case 1:
                _shape = new Square(_scale, _color);
                break;
            case 2:
                _shape = new Triangle(new Vector2(_scale, _scale / 2), Vector2.UnitY, _color);
                break;
            case 3:
                _shape = new Rect(new Vector2(_scale, _scale / 2), _color);
                break;
        }

        _shape!.UpdateShape(_position);

        _heading = new Vector2(rng.NextSingle() - rng.NextSingle(), rng.NextSingle() - rng.NextSingle());
    }

    #region Private Methods

    private (Vector2, Vector2) FindSpawnPointAsteroid(Vector2 origin, (int,int) worldDimensions, float angleTheta)
    {
        (float m,int b) = LineEquation(origin, angleTheta);
        // y = mx + b

        // if y = 0 x = variable
        // return (var, 0 pos, (diff between origin and intercept).normal);

        // if X = 0 y = variable
        // return (0, var, (diff between origin and intercept).normal);

        // if y = windowHeight x = variable
        // return (var, windowHeight, (diff between origin and intercept).normal)

        // if x = windowWidth y = variable
        // return (windowWidth, var, (diff between origin and intercept).normal)

        // repeat with window width/height
        Vector2 windowIntercept = Vector2.Zero;
        Vector2 heading = Vector2.Zero;

        // must be intercepting the 0s
        if (angleTheta >= Math.PI / 4 && angleTheta < Math.PI * (5 / 4))
        {
            if (b >= 0 && b <= worldDimensions.Item1)
            {
                windowIntercept = new Vector2(0,b); 
            }
            else if (m != 0)
            {
                var x = -b / m;
                windowIntercept = new Vector2(x, 0);
            }
        }
        else
        {
            // window width/height intercept
        }

        return (windowIntercept, heading);
    }

    private (float, int) LineEquation(Vector2 point, float angleTheta)
    {
        var vx = Math.Cos(angleTheta);
        var vy = Math.Sin(angleTheta);

        var slope = Math.Tan(angleTheta);

        var intercept = point.Y - slope * point.X;

        return (0,0);
    }

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

    public void ExplodeAsteroid()
    {

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

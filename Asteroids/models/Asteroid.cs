using System.Diagnostics;

using Raylib_CSharp.Colors;
using System.Numerics;
using AsteroidSharp.Models.Shapes;
using System.Security.Principal;

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
        //https://stackoverflow.com/questions/218060/random-gaussian-variables

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

    public (Vector2, Vector2) FindSpawnPointAsteroid(Vector2 origin, (int, int) worldDimensions, float angleTheta)
    {
        (double m, int b) = LineEquation(origin, angleTheta);

        (Vector2, Vector2) spawn = SpawnPoint(origin, worldDimensions, angleTheta, m, b);

        return spawn;
    }

    private (Vector2, Vector2) SpawnPoint(Vector2 origin, (int, int) worldDimensions, float angleTheta, double m, int b)
    {
        (var windowWidth, var windowHeight) = worldDimensions;
        Vector2 collision = Vector2.Zero;

        //awkward edge cases
        if (double.IsNaN(m))
        {
            var compPi = MathF.PI / 2;

            if (angleTheta == compPi)
            {
                // from the top
                return (new Vector2(origin.X, 0), Vector2.UnitY);
            }
            else
            {
                // from the bottom
                return (new Vector2(origin.X, 0), -Vector2.UnitY);
            }
        }

        if (m == 0)
        {
            if(angleTheta == Math.PI)
            {
                collision = new Vector2(0,origin.Y); 
                return (collision, -Vector2.UnitX);
            }
            else
            {
                collision = new Vector2(windowWidth,origin.Y);
                return (collision, Vector2.UnitX);
            }
        }

        // first quadrant
        if (angleTheta >= 0 && angleTheta < Math.PI / 2)
        {
            //must intersect with top or right
            
            // y = mx + b
            // right window = y = width
            var y = m*windowWidth + b;
            if(y >= 0 && y <= windowHeight)
            {
                // the case that right window border is collided and not negative
                collision = new Vector2(windowWidth, (float)y);
                return (collision,  CalculateHeading(origin, collision));
            }
            
            // m should never be zero
            var x = b/m;

            if (x >= 0 && x <= windowWidth)
            {
                collision = new Vector2((float)x, 0);
                return (collision, CalculateHeading(origin, collision));
            }
        }

        // second Quadrant
        if (angleTheta >= Math.PI / 2 && angleTheta < Math.PI)
        {
            // must intersect with top or left

            var y = b;
            if (y >= 0 && y <= windowHeight)
            {
                // collision with left side of window
                collision = new Vector2(0, y);
                return (collision, CalculateHeading(origin, collision));
            }

            var x = b/m;
            if (x >= 0 && x < windowWidth)
            {
                collision = new Vector2((float)x, 0);
                return (collision, CalculateHeading(origin, collision));
            }
        }

        // Third Quadrant
        if (angleTheta >= Math.PI && angleTheta < 3 * Math.PI / 2)
        {
            // must intersect with left or bottom
            var y = b;
            if (y >= 0 && y <= windowHeight)
            {
                collision = new Vector2(0, y);
                return (collision, CalculateHeading(origin, collision));
            }

            var x = windowHeight - b / m;
            if (x >= 0 && x <= windowWidth)
            {
                collision = new Vector2((float)x, windowHeight);
                return (collision, CalculateHeading(origin, collision));
            }
        }

        // Fourth Quadrant
        if (angleTheta >= 3 * Math.PI / 2 && angleTheta < 2 * Math.PI)
        {
            // must intersect with bottom or right
            var y = m*windowWidth + b;
            if (y >= 0 && y <= windowHeight)
            {
                collision = new Vector2(windowWidth, (float)y);
                return (collision, CalculateHeading(origin, collision));
            }

            var x = windowHeight - b / m;
            if (x >= 0 && x <= windowWidth)
            {
                collision = new Vector2((float)x, windowHeight);
                return (collision, CalculateHeading(origin, collision));
            }
        }

        return (Vector2.Zero, Vector2.Zero);
    }

    private (double, int) LineEquation(Vector2 point, float angleTheta)
    {
        double slope;
        int intercept;

        if (Math.Tan(angleTheta) - double.MaxValue < 1e-10 && angleTheta != 0 && angleTheta != MathF.PI)
        {
            slope = double.NaN;
            intercept = int.MaxValue;
        }
        else
        {
            slope = Math.Tan(angleTheta);
            intercept = (int)(point.Y - slope * point.X);
        }

        return (slope,intercept);
    }

    private Vector2 CalculateHeading(Vector2 origin, Vector2 intercept)
    {
        Vector2 difference = intercept - origin;
        return Vector2.Normalize(difference);
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

    public (Vector2, Vector2) DebugAsteroidSpawn(Vector2 origin, (int, int) worldDimensions, float angleTheta)
    {
        (Vector2, Vector2) spawn = FindSpawnPointAsteroid(origin, worldDimensions, angleTheta);

        return spawn;
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

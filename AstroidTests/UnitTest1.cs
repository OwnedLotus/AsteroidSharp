using System.Numerics;
using AsteroidSharp;
using AsteroidSharp.Models;

namespace AstroidTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Assert.True(1 == 1, "Sanity check" );
    }

    [Fact]
    public void TestCollisions()
    {
        List<Asteroid> asteroids =  Asteroid.DebugAsteroidSpawner();

        List<Vector2> vectors = new List<Vector2>{ Vector2.Zero};

        foreach (var asteroid in asteroids)
        {
            asteroid.CheckCollisions(vectors);
        }

    }
}
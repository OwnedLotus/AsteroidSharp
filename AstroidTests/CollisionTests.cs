using System.Numerics;
using AsteroidSharp.Models;

namespace AstroidTests;

public class CollisionTests
{
    [Fact]
    public void Test_ShouldCircleCollisionsHit()
    {
        Asteroid asteroid =  Asteroid.DebugCircleAsteroidSpawner();

        List<Vector2> vectors = new List<Vector2> { Vector2.Zero };
        asteroid.Move(1f);

        Assert.True(asteroid.CheckCollisions(vectors), "Circle Collision failed");
    }

    [Fact]
    public void Test_ShouldSquareCollisionsHit()
    {
        // Given
        Asteroid asteroid = Asteroid.DebugSquareAsteroidSpawner();
    
        // When
        List<Vector2> vectors = new List<Vector2> { Vector2.Zero };
        asteroid.Move(1f);
    
        // Then
        Assert.True(asteroid.CheckCollisions(vectors), "SSquare Collision Failed");
    }

    [Fact]
    public void Test_ShouldRectangleCollisionsHit()
    {
        // Given
        Asteroid asteroid = Asteroid.DebugRectangleAsteroidSpawner();
    
        // When
        List<Vector2> vectors = new List<Vector2> { Vector2.Zero };
        asteroid.Move(1f);
    
        // Then
        Assert.True(asteroid.CheckCollisions(vectors), "Rectangle Collision Failed");
    }

    [Fact]
    public void TestTriangleCollisions()
    {
        // Given
        Asteroid asteroid = Asteroid.DebugTriangleAsteroidSpawner();
    
        // When
        List<Vector2> vectors = new List<Vector2> { Vector2.Zero };
        asteroid.Move(1f);
    
        // Then
        Assert.True(asteroid.CheckCollisions(vectors), "Triangle Collision Failed");
    }
}
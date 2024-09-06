using System.Numerics;
using AsteroidSharp.Models;

namespace AstroidTests;

public class AsteroidTests
{
    [Fact]
    public void SanityCheck()
    {
        // Given
    
        // When
    
        // Then
        Assert.True(1 == 1, "Tautology");
    }

    private (Vector2, Vector2) FindSpawnPointAsteroid(Vector2 origin, (int, int) worldDimensions, float angleTheta)
    {
        // Your implementation here
        throw new NotImplementedException();
    }

    [Theory]
    [InlineData(0, 0, 800, 600, 0, 800, 0)] // Right border
    [InlineData(0, 0, 800, 600, (float)Math.PI / 2, 0, 600)] // Top border
    [InlineData(0, 0, 800, 600, (float)Math.PI, 0, 0)] // Left border
    [InlineData(0, 0, 800, 600, (float)(3 * Math.PI / 2), 0, 0)] // Bottom border
    public void TestFindSpawnPointAsteroid(float originX, float originY, int worldWidth, int worldHeight, float angleTheta, float expectedX, float expectedY)
    {
        // Arrange
        Vector2 origin = new Vector2(originX, originY);
        (int, int) worldDimensions = (worldWidth, worldHeight);

        var asteroid = new Asteroid();

        // Act
        (Vector2 intercept, Vector2 heading) = asteroid.DebugAsteroidSpawn(origin, worldDimensions, angleTheta);

        // Assert
        Assert.Equal(new Vector2(expectedX, expectedY), intercept);
        Assert.True(heading.Length() > 0); // Ensure heading is a unit vector
    }    
}
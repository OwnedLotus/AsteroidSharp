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
    [InlineData(400, 300, 800, 600, 0, 800, 300, MathF.PI)] // Right border
    [InlineData(400, 300, 800, 600, MathF.PI / 2, 400, 0, 3 * MathF.PI / 2)] // Top border
    [InlineData(400, 300, 800, 600, MathF.PI, 0, 300, 0)] // Left border
    [InlineData(400, 300, 800, 600, (3 * MathF.PI / 2), 400, 600, MathF.PI / 2)] // Bottom border
    public void TestFindSpawnPointAsteroid(float originX, float originY, int worldWidth, int worldHeight, float angleTheta, float expectedX, float expectedY, float expectedHeadingAngle)
    {
        // Arrange
        Vector2 origin = new Vector2(originX, originY);
        (int, int) worldDimensions = (worldWidth, worldHeight);
        var asteroid = new Asteroid();
        var expectedLocation = new Vector2(expectedX, expectedY);

        // Act
        (Vector2 intercept, Vector2 heading) = asteroid.DebugAsteroidSpawn(origin, worldDimensions, angleTheta);

        // Assert
        Assert.Equal(expectedLocation, intercept);
        // Assert.Equal(expectedHeadingAngle, Vector2.Dot(origin, heading));
    }    
}
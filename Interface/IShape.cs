using System.Numerics;
using AsteroidSharp.Models;

public interface IShape
{
    

    public int[] Bounds { get; }

    public void UpdateShape(Vector2 pos);
    public void DrawShape();
    public void RotateShape(Vector2 pos, float rotateSpeed);
}

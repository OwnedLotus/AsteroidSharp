using AsteroidSharp.Models;

public interface IShape
{
    public Position pos { get; set; }
    public float[] bounds { get; set; }

    public float GetArea();
    public int[] GetDimentsions();
    public void DrawShape();
}

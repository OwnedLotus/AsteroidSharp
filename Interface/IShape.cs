public interface IShape
{
    public float[] bounds { get; set; }

    public float GetArea();
    public int[] GetDimentsions();
    public void DrawShape();
}
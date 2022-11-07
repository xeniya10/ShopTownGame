namespace ShopTown.ModelComponent
{
public class GameBoardModel
{
    public int Rows;
    public int Columns;
    private float _paddingRatio = 0.02f;
    private const int _widthReference = 800;
    private int _padding { get { return (int)(_widthReference * _paddingRatio); } }

    public float CalculateCellSize()
    {
        float cellSpace = _widthReference - 2 * _padding;
        var cellSize = cellSpace / Columns;
        return cellSize;
    }

    public float[] CalculateCellPosition(float xFactor, float yFactor, float cellSize)
    {
        var x = _widthReference / 2 - _padding - cellSize * (xFactor + 0.5f);
        var y = _widthReference / 2 - cellSize * yFactor;

        // var cellPosition = new Vector2(x, y);
        var cellPosition = new float[] {x, y};
        return cellPosition;
    }
}
}

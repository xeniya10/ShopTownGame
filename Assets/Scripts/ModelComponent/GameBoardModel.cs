using UnityEngine;

public class GameBoardModel
{
    public int Rows = 4;
    public int Columns = 3;
    private int _screenWidth = 800;
    private int _padding;
    private float _paddingRatio = 0.02f;

    public float CalculateCellSize()
    {
        _padding = (int)(_screenWidth * _paddingRatio);

        float cellSpace = _screenWidth - 2 * _padding;
        var cellSize = cellSpace / Columns;

        return cellSize;
    }

    public Vector2 CalculateCellPosition(float xFactor, float yFactor, float cellSize)
    {
        var x = _screenWidth / 2 - _padding - cellSize * (xFactor + 0.5f);
        var y = _screenWidth / 2 - cellSize * yFactor;

        var cellPosition = new Vector2(x, y);
        return cellPosition;
    }
}
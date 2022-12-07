using UnityEngine;

namespace ShopTown.ModelComponent
{
public class SplashBoardModel
{
    public int Rows;
    public int Columns;
    private float _sizeFactor = 1.3f;
    private const int _widthReference = 800;
    private float _cellSize;

    public SplashBoardModel(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
    }

    public float CalculateCellSize()
    {
        _cellSize = _sizeFactor * _widthReference / Columns;
        return _cellSize;
    }

    private void CheckCellSize()
    {
        if (_cellSize == 0)
        {
            CalculateCellSize();
        }
    }

    public Vector2 CalculateStartPosition(float xFactor, float yFactor)
    {
        CheckCellSize();
        var x = -_widthReference - _cellSize * (xFactor + 1);
        var y = Screen.height - _cellSize * (yFactor + 1);
        return new Vector2(x, y);
    }

    public Vector2 CalculateTargetPosition(float xFactor, float yFactor)
    {
        CheckCellSize();
        var x = 0.5f * Columns * _cellSize / _sizeFactor - _cellSize * xFactor;
        var y = 0.5f * Rows * _cellSize - _cellSize * yFactor;
        return new Vector2(x, y);
    }
}
}

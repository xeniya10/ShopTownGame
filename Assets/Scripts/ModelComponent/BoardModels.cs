using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace ShopTown.ModelComponent
{
public class GameBoardModel
{
    public int Rows = 4;
    public int Columns = 3;
    private float _paddingRatio = 0.02f;
    private const int _widthReference = 800;
    private int _padding;

    public float CalculateCellSize()
    {
        _padding = (int)(_widthReference * _paddingRatio);
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

public class SplashBoardModel
{
    public int Rows = 7;
    public int Columns = 4;
    private float _sizeFactor = 1.3f;
    private const int _widthReference = 800;
    private float _cellSize;

    public float CalculateCellSize()
    {
        _cellSize = _sizeFactor * _widthReference / Columns;
        return _cellSize;
    }

    public Vector2 CalculateStartPosition(float xFactor, float yFactor)
    {
        var x = -_widthReference - _cellSize * (xFactor + 1);
        var y = Screen.height - _cellSize * (yFactor + 1);

        var cellPosition = new Vector2(x, y);
        return cellPosition;
    }

    public Vector2 CalculateTargetPosition(float xFactor, float yFactor)
    {
        var x = 0.5f * Columns * _cellSize / _sizeFactor - _cellSize * xFactor;
        var y = 0.5f * Rows * _cellSize - _cellSize * yFactor;

        var cellPosition = new Vector2(x, y);
        return cellPosition;
    }
}
}

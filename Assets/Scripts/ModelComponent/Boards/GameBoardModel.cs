using System;

namespace ShopTown.ModelComponent
{
[Serializable]
public class GameBoardModel
{
    public int Rows;
    public int Columns;
    private int _padding;
    private float _cellSize;
    private float _paddingRatio = 0.02f;
    private const int _widthReference = 800;

    public float CalculateCellSize()
    {
        _padding = (int)(_widthReference * _paddingRatio);
        float cellSpace = _widthReference - 2 * _padding;
        _cellSize = cellSpace / Columns;
        return _cellSize;
    }

    private void CheckCellSize()
    {
        if (_cellSize == 0)
        {
            CalculateCellSize();
        }
    }

    public float[] CalculateCellPosition(float xFactor, float yFactor)
    {
        CheckCellSize();
        var x = _widthReference / 2 - _padding - _cellSize * (xFactor + 0.5f);
        var y = _widthReference / 2 - _cellSize * yFactor;
        return new[] {x, y};
    }
}
}

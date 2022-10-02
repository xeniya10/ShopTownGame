using System;
using UnityEngine;

[Serializable]
public class GameBoardModel
{
    public int Rows;
    public int Columns;
    private const int _widthReference = 800;
    private int _padding;
    public float PaddingRatio = 0.02f;

    public float CalculateCellSize()
    {
        _padding = (int)(_widthReference * PaddingRatio);

        float cellSpace = _widthReference - 2 * _padding;
        var cellSize = cellSpace / Columns;

        return cellSize;
    }

    public Vector2 CalculateCellPosition(float xFactor, float yFactor, float cellSize)
    {
        var x = _widthReference / 2 - _padding - cellSize * (xFactor + 0.5f);
        var y = _widthReference / 2 - cellSize * yFactor;

        var cellPosition = new Vector2(x, y);
        return cellPosition;
    }
}
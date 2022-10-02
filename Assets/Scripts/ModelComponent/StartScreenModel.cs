using System;
using UnityEngine;

[Serializable]
public class StartScreenModel
{
    public int Rows = 7;
    public int Columns = 4;
    private const int _widthReference = 800;
    public float SizeFactor = 1.3f;

    public float CalculateCellSize()
    {
        var cellSize = SizeFactor * _widthReference / Columns;
        return cellSize;
    }

    public Vector2 CalculateStartPosition(float xFactor, float yFactor, float cellSize)
    {
        var x = -_widthReference - cellSize * (xFactor + 1);
        var y = Screen.height - cellSize * (yFactor + 1);

        var cellPosition = new Vector2(x, y);
        return cellPosition;
    }

    public Vector2 CalculateTargetPosition(float xFactor, float yFactor, float cellSize)
    {
        var x = Columns / 2 * cellSize / SizeFactor - cellSize * xFactor;
        var y = Rows / 2 * cellSize - cellSize * yFactor;

        var cellPosition = new Vector2(x, y);
        return cellPosition;
    }
}

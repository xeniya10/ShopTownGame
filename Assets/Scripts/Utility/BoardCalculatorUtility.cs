using UnityEngine;

public static class BoardCalculatorUtility
{
    private static int _padding = (int)(Screen.width * 0.1f);
    private static int _spacing = (int)(Screen.width * 0.01f);

    public static float CalculateCellSize(int columnNumber)
    {
        int paddingNumber = 2;
        int spacingNumber = columnNumber - 1;

        float cellSpace = Screen.width - paddingNumber * _padding - spacingNumber * _spacing;
        var cellSize = cellSpace / columnNumber;
        return cellSize;

    }

    /// <summary>Offset between local position of two nearest cells.</summary>
    public static float CalculateCellOffset(float cellSize)
    {
        var cellOffset = cellSize + _spacing;
        return cellOffset;
    }

    public static Vector2 CalculateStartCellPosition(float cellSize)
    {
        var startCellPosition = new Vector2();

        startCellPosition.x = -Screen.width / 2 + cellSize / 2 + _padding;
        startCellPosition.y = cellSize / 2 + _padding;

        return startCellPosition;
    }
}
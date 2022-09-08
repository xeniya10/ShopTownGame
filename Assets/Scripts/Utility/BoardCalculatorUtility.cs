using UnityEngine;

public static class BoardCalculatorUtility
{
    // public static int Padding = (int)(Screen.width * 0.1f);
    // public static int Spacing = (int)(Screen.width * 0.01f);

    public static float CalculateCellSize(int columnNumber, float padding, float spacing)
    {
        int paddingNumber = 2;
        int spacingNumber = columnNumber - 1;

        float cellSpace = Screen.width - paddingNumber * padding - spacingNumber * spacing;
        var cellSize = cellSpace / columnNumber;
        return cellSize;

    }

    /// <summary>Offset between local position of two nearest cells.</summary>
    public static float CalculateCellOffset(float cellSize, float spacing)
    {
        var cellOffset = cellSize + spacing;
        return cellOffset;
    }

    public static Vector2 CalculateStartCellPosition(float cellSize, float padding)
    {
        var startCellPosition = new Vector2();

        startCellPosition.x = -Screen.width / 2 + cellSize / 2 + padding;
        startCellPosition.y = cellSize / 2 + padding;

        return startCellPosition;
    }
}
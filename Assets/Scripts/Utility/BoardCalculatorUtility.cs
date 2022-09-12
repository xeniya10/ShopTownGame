using UnityEngine;

public static class BoardCalculatorUtility
{
    // public static int Padding = (int)(Screen.width * 0.1f);
    // public static int Spacing = (int)(Screen.width * 0.01f);

    public static float CalculateCellSize(int numberOfRow, int numberOfColumn, float padding, float spacing, float sizeFactor)
    {
        var numberOfPadding = 2;
        var numberOfSpacing = numberOfColumn - 1;

        float cellSpace = Screen.width - numberOfPadding * padding - numberOfSpacing * spacing;
        var cellSize = sizeFactor * cellSpace / numberOfColumn;

        if (cellSize * numberOfRow <= Screen.height)
        {
            cellSpace = Screen.height - numberOfPadding * padding - numberOfSpacing * spacing;
            cellSize = sizeFactor * cellSpace / numberOfRow;
        }

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

    public static float CalculateXPosition(float widthFactor, float cellSize, float cellFactor)
    {
        var xPosition = Screen.width * widthFactor - cellSize * cellFactor;
        return xPosition;
    }

    public static float CalculateYPosition(float heightFactor, float cellSize, float cellFactor)
    {
        var yPosition = Screen.height * heightFactor - cellSize * cellFactor;
        return yPosition;
    }
}
using System;
using UnityEngine;

namespace ShopTown.ModelComponent
{
[Serializable]
public class SplashScreenModel
{
    public int Rows = 7;
    public int Columns = 4;
    public float SizeFactor = 1.3f;
    private const int _widthReference = 800;
    public float CellSize { get { return SizeFactor * _widthReference / Columns; } }

    public Vector2 CalculateStartPosition(float xFactor, float yFactor)
    {
        var x = -_widthReference - CellSize * (xFactor + 1);
        var y = Screen.height - CellSize * (yFactor + 1);

        var cellPosition = new Vector2(x, y);
        return cellPosition;
    }

    public Vector2 CalculateTargetPosition(float xFactor, float yFactor)
    {
        var x = 0.5f * Columns * CellSize / SizeFactor - CellSize * xFactor;
        var y = 0.5f * Rows * CellSize - CellSize * yFactor;

        var cellPosition = new Vector2(x, y);
        return cellPosition;
    }
}
}

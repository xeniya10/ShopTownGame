using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameCellData", menuName = "GameCellScriptableObject")]

public class GameCellData : ScriptableObject
{
    public double[] BaseProfit =
    {
        1,
        6,
        14,
        25,
        80,
        140,
        300,
        720,
        2000,
        4100,
        10000,
        20000,
        50000,
        150000,
        500000,
        1000000,
        2000000,
        5000000,
        10000000,
        20000000,
        40000000,
        90000000,
        150000000,
        300000000,
        500000000,
        1000000000,
        1500000000
    };

    public TimeSpan[] ProcessTime =
    {
        new TimeSpan(0,0,2),
        new TimeSpan(0,0,5),
        new TimeSpan(0,0,10),
        new TimeSpan(0,0,20),
        new TimeSpan(0,0,40),
        new TimeSpan(0,1,0),
        new TimeSpan(0,2,0),
        new TimeSpan(0,3,0),
        new TimeSpan(0,5,0),
        new TimeSpan(0,8,0),
        new TimeSpan(0,10,0),
        new TimeSpan(0,15,0),
        new TimeSpan(0,20,0),
        new TimeSpan(0,40,0),
        new TimeSpan(1,0,0),
        new TimeSpan(2,0,0),
        new TimeSpan(4,0,0),
        new TimeSpan(6,0,0),
        new TimeSpan(8,0,0),
        new TimeSpan(10,0,0),
        new TimeSpan(14,0,0),
        new TimeSpan(18,0,0),
        new TimeSpan(24,0,0),
        new TimeSpan(30,0,0),
        new TimeSpan(36,0,0),
        new TimeSpan(42,0,0),
        new TimeSpan(48,0,0)
    };

    public double[] Cost =
    {
        2,
        50,
        100,
        500,
        1500,
        3000,
        8000,
        15000,
        40000,
        100000,
        300000,
        1000000,
        3000000,
        10000000,
        20000000,
        50000000,
        100000000,
        200000000,
        500000000,
        1000000000
    };
}
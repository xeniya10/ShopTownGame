using System;
using System.Collections.Generic;
using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "BoardData", menuName = "BoardData")]
public class BoardData : ScriptableObject, IBoardData
{
    [SerializeField] private GameBoardModel _defaultBoard = new GameBoardModel();
    [SerializeField] private GameCellModel _defaultCell = new GameCellModel();
    [SerializeField] private List<MoneyModel> _profit = new List<MoneyModel>();
    [SerializeField] private List<MoneyModel> _cost = new List<MoneyModel>();
    [SerializeField] private List<TimeModel> _time = new List<TimeModel>();

    public GameBoardModel GetDefaultBoard()
    {
        if (_defaultBoard == null)
        {
            throw new NullReferenceException($"{GetType().Name}.{nameof(GetDefaultBoard)}: Object is null");
        }

        return _defaultBoard;
    }

    public GameCellModel GetDefaultCell()
    {
        if (_defaultCell == null)
        {
            throw new NullReferenceException($"{GetType().Name}.{nameof(GetDefaultCell)}: Object is null");
        }

        return _defaultCell;
    }

    public MoneyModel GetCellProfit(int level)
    {
        if (_profit == null || _profit.Count == 0 || level > _profit.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetCellProfit)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _profit[level - 1];
    }

    public int GetCostCount()
    {
        if (_cost == null)
        {
            throw new NullReferenceException($"{GetType().Name}.{nameof(GetCostCount)}: List is null");
        }

        return _cost.Count;
    }

    public MoneyModel GetCellCost(int number)
    {
        if (_cost == null || _cost.Count == 0 || number > _cost.Count - 1)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetCellCost)}: List is null/empty or input number: {number} greater, then count of list");
        }

        return _cost[number];
    }

    public TimeModel GetCellTime(int level)
    {
        if (_time == null || _time.Count == 0 || level > _time.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetCellTime)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _time[level - 1];
    }
}
}

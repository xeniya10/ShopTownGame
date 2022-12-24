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
        return _defaultBoard;
    }

    public GameCellModel GetDefaultCell()
    {
        return _defaultCell;
    }

    public MoneyModel GetProfit(int level)
    {
        if (_profit.Count == 0 || level > _profit.Count)
        {
            Debug.Log("List of Profit is empty or input level greater, then count of list");
            return null;
        }

        return _profit[level - 1];
    }

    public int GetCostCount()
    {
        return _cost.Count;
    }

    public MoneyModel GetCost(int number)
    {
        if (_cost.Count == 0 || number > _cost.Count - 1)
        {
            Debug.Log("List of Cost is empty or input level greater, then count of list");
            return null;
        }

        return _cost[number];
    }

    public TimeModel GetTime(int level) // naming
    {
        if (_time.Count == 0 || level > _time.Count)
        {
            Debug.Log("List of Time is empty or input level greater, then count of list");
            return null;
        }

        return _time[level - 1];
    }
}
}

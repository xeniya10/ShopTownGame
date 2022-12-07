using System.Collections.Generic;
using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "BoardData", menuName = "BoardData")]
public class BoardData : ScriptableObject
{
    public GameBoardModel DefaultBoard = new GameBoardModel();

    public GameCellModel DefaultCell = new GameCellModel();

    public List<MoneyModel> BaseProfit = new List<MoneyModel>();

    public List<MoneyModel> Cost = new List<MoneyModel>();

    public List<TimeModel> ProcessTime = new List<TimeModel>();
}
}

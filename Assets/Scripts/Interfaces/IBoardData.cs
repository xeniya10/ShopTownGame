using ShopTown.ModelComponent;

namespace ShopTown.Data
{
public interface IBoardData
{
    GameBoardModel GetDefaultBoard();

    GameCellModel GetDefaultCell();

    MoneyModel GetProfit(int level);

    int GetCostCount();

    MoneyModel GetCost(int number);

    TimeModel GetTime(int level);
}
}

using ShopTown.ModelComponent;

namespace ShopTown.Data
{
public interface IBoardData
{
    GameBoardModel GetDefaultBoard();

    GameCellModel GetDefaultCell();

    MoneyModel GetCellProfit(int level);

    int GetCostCount();

    MoneyModel GetCellCost(int number);

    TimeModel GetCellTime(int level);
}
}

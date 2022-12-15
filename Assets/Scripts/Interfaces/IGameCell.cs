using System;
using ShopTown.Data;
using ShopTown.ModelComponent;

namespace ShopTown.PresenterComponent
{
public interface IGameCell
{
    public GameCellModel Model { get; }
    public event Action ChangeEvent;
    public event Action<MoneyModel> InProgressEndEvent;
    public event Action<MoneyModel> GetOfflineProfitEvent;

    void SetState(CellState state, BoardData cellData, Action callBack = null);

    void LevelUp(BoardData cellData);

    void InitializeManager(ImprovementModel manager, BoardData cellData);

    void InitializeUpgrade(ImprovementModel upgrade, BoardData cellData);

    void SetCost(int activationNumber, BoardData cellData);

    void SubscribeToBuyButton(Action<GameCellPresenter> callBack);

    void SubscribeToCellClick(Action<GameCellPresenter> callBack);

    void SetActiveSelector(bool isActive);

    bool IsNeighborOf(GameCellPresenter cell);

    bool HasSameLevelAs(GameCellPresenter anotherCell);
}
}

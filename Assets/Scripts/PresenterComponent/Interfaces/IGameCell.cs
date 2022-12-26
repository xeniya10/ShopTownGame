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

    void SetState(CellState state, IBoardData cellData, Action onAnimationEndedCallback = null);

    void LevelUp(IBoardData cellData);

    void InitializeManager(ImprovementModel manager, IBoardData cellData);

    void InitializeUpgrade(ImprovementModel upgrade, IBoardData cellData);

    void SetCost(int activatedCellsNumber, IBoardData cellData);

    void SubscribeToBuyButton(IButtonSubscriber subscriber, Action<IGameCell> callBack);

    void SubscribeToCellClick(IButtonSubscriber subscriber, Action<IGameCell> callBack);

    void SetActiveSelector(bool isActive);

    bool IsNeighborOf(IGameCell cell);

    bool HasSameLevelAs(IGameCell anotherCell);
}
}

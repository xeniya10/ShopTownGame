using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public interface IModel
{}

public interface IManager
{}

public interface IUpgrade
{}

public interface IImprovement
{
    public ImprovementModel Model { get; }
    public event Action ChangeEvent;

    void Initialize(ImprovementData improvementData, ImprovementContainer improvementSprites);

    void SubscribeToBuyButton(Action<ImprovementPresenter> callBack);

    void SetState(ImprovementState state);

    void Activate();
}

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

    bool HasSameLevelAs(GameCellPresenter otherCell);
}

public interface IPresenterFactory<out T>
{
    T Create(IModel model, IView view);
}

public class GameCellFactory : IPresenterFactory<IGameCell>
{
    public IGameCell Create(IModel model, IView view)
    {
        return new GameCellPresenter(model, view);
    }
}

public class ManagerFactory : IPresenterFactory<IManager>
{
    public IManager Create(IModel model, IView view)
    {
        return new ManagerPresenter(model, view);
    }
}

public class UpgradeFactory : IPresenterFactory<IUpgrade>
{
    public IUpgrade Create(IModel model, IView view)
    {
        return new UpgradePresenter(model, view);
    }
}
}

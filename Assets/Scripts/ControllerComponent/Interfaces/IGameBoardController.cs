using System;
using ShopTown.ControllerComponent;
using ShopTown.ModelComponent;

public interface IGameBoardController : IDisposable
{
    event Action<GameCellModel> CellActivationEvent;
    event Action<int, bool> CellUnlockEvent;
    event Action<IGameData> SetOfflineProfitEvent;

    void Initialize();

    void InitializeManager(ImprovementModel improvement);

    void InitializeUpgrade(ImprovementModel improvement);
}

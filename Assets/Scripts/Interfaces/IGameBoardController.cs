using System;
using ShopTown.ModelComponent;

public interface IGameBoardController : IDisposable
{
    event Action<GameCellModel> ActivateEvent;
    event Action<int, bool> UnlockEvent;
    event Action SetOfflineProfitEvent;

    void Initialize();

    void InitializeManager(ImprovementModel improvement);

    void InitializeUpgrade(ImprovementModel improvement);
}

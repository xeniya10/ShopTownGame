using System;
using ShopTown.ControllerComponent;
using ShopTown.ModelComponent;

public interface IGameBoardController : IDisposable
{
    event Action<GameCellModel> ActivateEvent; // Seems as gameboard activate event (naming)
    event Action<int, bool> UnlockEvent; // Seems as gameboard activate event (naming)
    event Action<IGameData> SetOfflineProfitEvent;  

    void Initialize();

    void InitializeManager(ImprovementModel improvement);

    void InitializeUpgrade(ImprovementModel improvement);
}

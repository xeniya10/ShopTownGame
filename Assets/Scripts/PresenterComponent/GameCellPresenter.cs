using System;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.PresenterComponent
{
public class GameCellPresenter : ICreatable<GameCellPresenter, GameCellModel>
{
    [Inject]
    private readonly GameScreenView _gameScreen;
    private readonly GameCellView _view;
    public readonly GameCellModel Model;

    private GameCellPresenter(GameCellView view, GameCellModel model)
    {
        _view = view;
        Model = model;
    }

    public GameCellPresenter Create(GameCellModel model)
    {
        _view.SetSize(model.Size);
        var view = _view.Create(_gameScreen.GameBoard);

        if (model.BackgroundNumber < 0)
        {
            var spriteNumber = view.RandomBackgroundNumber();
            model.BackgroundNumber = spriteNumber;
        }

        view.StartAnimation(model);
        view.SetActiveImprovements(model);
        var presenter = new GameCellPresenter(view, model);
        return presenter;
    }

    public void SetState(CellState state, Action callBack = null)
    {
        Model.SetState(state);
        if (state == CellState.InProgress)
        {
            GetInProgress(callBack);
            return;
        }

        _view.StartAnimation(Model, callBack);
    }

    private void GetInProgress(Action callBack)
    {
        _view.StartAnimation(Model, () =>
        {
            callBack?.Invoke();
            if (Model.IsManagerActivated)
            {
                GetInProgress(callBack);
                return;
            }

            Model.SetState(CellState.Active);
        });
    }

    public void LevelUp()
    {
        Model.Level += 1;
        SetState(CellState.Active);
    }

    public void InitializeManager(ManagerRowModel manager)
    {
        Model.IsManagerActivated = manager.IsActivated;
        _view.SetActiveImprovements(Model);
    }

    public void InitializeUpgrade(UpgradeRowModel upgrade)
    {
        if (upgrade.AreAllLevelsActivated)
        {
            Model.ActivatedUpgradeLevel = upgrade.UpgradeLevel;
        }
        else
        {
            Model.ActivatedUpgradeLevel = upgrade.UpgradeLevel - 1;
        }

        Model.AreAllUpgradeLevelsActivated = upgrade.AreAllLevelsActivated;
        _view.SetActiveImprovements(Model);
    }

    public void SubscribeToBuyButton(Action<GameCellPresenter> callBack)
    {
        _view.BuyButton.onClick.AddListener(() => callBack?.Invoke(this));
    }

    public void SubscribeToClick(Action<GameCellPresenter> callBack)
    {
        _view.CellButton.onClick.AddListener(() => callBack?.Invoke(this));
    }

    public void SetActiveSelector(bool isActive)
    {
        _view._selectorImage.gameObject.SetActive(isActive);
    }

    public bool IsNeighborOf(GameCellPresenter cell)
    {
        var xDiff = Math.Abs(cell.Model.GridIndex[0] - Model.GridIndex[0]);
        var yDiff = Math.Abs(cell.Model.GridIndex[1] - Model.GridIndex[1]);

        if (xDiff == 1 && yDiff == 0 || xDiff == 0 && yDiff == 1)
        {
            return true;
        }

        return false;
    }

    public bool HasSameLevelAs(GameCellPresenter otherCell)
    {
        if (Model.Level == otherCell.Model.Level)
        {
            return true;
        }

        return false;
    }
}
}

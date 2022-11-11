using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.PresenterComponent
{
public interface ICreatable<P, M>
{
    P Create(M m);
}

public class GameCellPresenter : ICreatable<GameCellPresenter, GameCellModel>
{
    [Inject] private readonly GameScreenView _gameScreen;
    [Inject] private readonly GameCellData _cellData;
    private readonly GameCellView _view;
    public readonly GameCellModel Model;

    private GameCellPresenter(GameCellView view, GameCellModel model, GameCellData cellData)
    {
        _view = view;
        Model = model;
        _cellData = cellData;
    }

    public GameCellPresenter Create(GameCellModel model)
    {
        _view.SetSize(model.Size);
        var view = _view.Create(_gameScreen.GameBoard);
        view.StartAnimation(model);
        view.SetActiveImprovements(model);
        var presenter = new GameCellPresenter(view, model, _cellData);
        presenter.SetParameters();
        return presenter;
    }

    public void SetState(CellState state, Action callBack = null)
    {
        Model.SetState(state);
        SetParameters();
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
        SetParameters();
        _view.SetActiveImprovements(Model);
    }

    private void SetParameters()
    {
        SetTime();
        SetProfit();
    }

    private void SetTime()
    {
        if (Model.Level < 1)
        {
            Model.TotalTime = TimeSpan.Zero;
            return;
        }

        Model.TotalTime = _cellData.ProcessTime[Model.Level - 1];
    }

    public void SetCost(int activationNumber)
    {
        var costData = _cellData.Cost;

        if (activationNumber > costData.Count - 1)
        {
            var lastElement = _cellData.Cost[costData.Count - 1];
            Model.Cost = new MoneyModel(lastElement.Number * (activationNumber - costData.Count + 2),
                lastElement.Value);

            return;
        }

        Model.Cost = costData[activationNumber];
    }

    private void SetProfit()
    {
        if (Model.Level < 1)
        {
            Model.Profit = null;
            return;
        }

        var profitMultiplier = Model.ActivatedUpgradeLevel + 1;
        var baseProfit = _cellData.BaseProfit[Model.Level - 1];
        Model.Profit = new MoneyModel(baseProfit.Number * profitMultiplier, baseProfit.Value);
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

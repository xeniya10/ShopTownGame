using System;
using ShopTown.Data;
using ShopTown.ModelComponent;

namespace ShopTown.PresenterComponent
{
// T1 - presenter
// public interface ICreatable<T1, T2>
// {
//     T1 Create(Transform parent, T2 model);
// }

public class GameCellPresenter
{
    private readonly GameCellData _cellData;
    private readonly GameCellView _view;
    public readonly GameCellModel Model;

    public Action ModelChangeEvent;
    public Action<MoneyModel> InProgressAnimationEndEvent;

    public GameCellPresenter(GameCellView view, GameCellModel model, GameCellData cellData)
    {
        _view = view;
        Model = model;
        _cellData = cellData;
    }

    public void Initialize()
    {
        _view.StartAnimation(Model);
        _view.InitializeImprovements(Model);
        SetParameters();
    }
    //
    // public GameCellPresenter Create(Transform parent, GameCellModel model)
    // {
    //     var view = _view.Create(parent, model.Size);
    //     view.StartAnimation(model);
    //     view.InitializeImprovements(model);
    //     var presenter = new GameCellPresenter(view, ref model, _cellData);
    //     presenter.SetParameters();
    //     return presenter;
    // }

    public void SetState(CellState state, Action callBack = null)
    {
        Model.SetState(state);
        SetParameters();
        ModelChangeEvent?.Invoke();
        if (state == CellState.InProgress)
        {
            GetInProgress();
            return;
        }

        _view.StartAnimation(Model, callBack);
    }

    private void GetInProgress()
    {
        if (Model.IsManagerActivated)
        {
            var timeAfterStart = (float)DateTime.Now.Subtract(Model.StartTime).TotalSeconds;
            var progressTime = (float)Model.TotalTime.TotalSeconds;

            if (timeAfterStart > progressTime)
            {
                Model.StartTime = DateTime.Now.Subtract(TimeSpan.FromSeconds(timeAfterStart % progressTime));
            }
        }
        else
        {
            Model.StartTime = DateTime.Now;
        }

        _view.StartAnimation(Model, () =>
        {
            InProgressAnimationEndEvent?.Invoke(Model.Profit);
            if (Model.IsManagerActivated)
            {
                GetInProgress();
                return;
            }

            Model.SetState(CellState.Active);
        });
    }

    public void LevelUp()
    {
        Model.Level += 1;
        SetState(CellState.Active);
        _view.StartLevelUpAnimation(Model);
    }

    public void InitializeManager(ImprovementModel manager)
    {
        Model.IsManagerActivated = manager.IsActivated;
        if (Model.IsManagerActivated)
        {
            SetState(CellState.InProgress);
        }

        _view.InitializeImprovements(Model);
    }

    public void InitializeUpgrade(ImprovementModel upgrade)
    {
        Model.ActivatedUpgradeLevel = upgrade.ImprovementLevel - 1;
        if (upgrade.IsActivated)
        {
            Model.ActivatedUpgradeLevel = upgrade.ImprovementLevel;
        }

        Model.AreAllUpgradeLevelsActivated = upgrade.IsActivated;
        SetParameters();

        _view.InitializeImprovements(Model);
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
        if (activationNumber > _cellData.Cost.Count - 1)
        {
            var lastElement = _cellData.Cost[_cellData.Cost.Count - 1];
            Model.Cost = new MoneyModel(lastElement.Number * (activationNumber - _cellData.Cost.Count + 2),
                lastElement.Value);

            return;
        }

        Model.Cost = _cellData.Cost[activationNumber];
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

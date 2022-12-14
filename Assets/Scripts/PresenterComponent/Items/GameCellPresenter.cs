using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public class GameCellPresenter : ButtonSubscription, IGameCell
{
    private GameCellModel _model;
    private IGameCellView _view;

    public GameCellModel Model { get { return _model; } }

    public event Action ChangeEvent;
    public event Action<MoneyModel> InProgressEndEvent;
    public event Action<MoneyModel> GetOfflineProfitEvent;

    public GameCellPresenter(IModel model, IView view)
    {
        _model = (GameCellModel)model;
        _view = (IGameCellView)view;
    }

    public void SetState(CellState state, BoardData cellData, Action callBack = null)
    {
        Model.State = state;
        SetParameters(cellData);
        switch (state)
        {
            case CellState.Lock or CellState.Unlock:
                Model.SetDefaultData(cellData.DefaultCell);
                Model.State = state;
                break;

            case CellState.InProgress:
                CalculateStartTime();
                callBack = GetInProgressCallBack;
                break;
        }

        ChangeEvent?.Invoke();
        _view.StartAnimation(Model, callBack);
    }

    public void LevelUp(BoardData cellData)
    {
        Model.Level += 1;
        SetState(CellState.Active, cellData);
        _view.StartLevelUpAnimation(Model);
    }

    public void InitializeManager(ImprovementModel manager, BoardData cellData)
    {
        Model.IsManagerActivated = manager.IsActivated;
        if (Model.IsManagerActivated)
        {
            SetState(CellState.InProgress, cellData);
        }

        _view.InitializeImprovements(Model);
    }

    public void InitializeUpgrade(ImprovementModel upgrade, BoardData cellData)
    {
        Model.ActivatedUpgradeLevel = upgrade.ImprovementLevel - 1;
        if (upgrade.IsActivated)
        {
            Model.ActivatedUpgradeLevel = upgrade.ImprovementLevel;
        }

        Model.AreAllUpgradeLevelsActivated = upgrade.IsActivated;
        SetParameters(cellData);
        _view.InitializeImprovements(Model);
    }

    public void SetCost(int activationNumber, BoardData cellData)
    {
        if (activationNumber > cellData.Cost.Count - 1)
        {
            var lastElement = cellData.Cost[cellData.Cost.Count - 1];
            var costNumber = lastElement.Number * (activationNumber - cellData.Cost.Count + 2);
            Model.Cost = new MoneyModel(costNumber, lastElement.Value);
            return;
        }

        Model.Cost = cellData.Cost[activationNumber];
    }

    public void SubscribeToBuyButton(Action<GameCellPresenter> callBack)
    {
        SubscribeToButton(_view.GetBuyButton(), () => callBack?.Invoke(this));
    }

    public void SubscribeToCellClick(Action<GameCellPresenter> callBack)
    {
        SubscribeToButton(_view.GetCellButton(), () => callBack?.Invoke(this));
    }

    public void SetActiveSelector(bool isActive)
    {
        _view.SetActiveSelector(isActive);
    }

    private void CalculateStartTime()
    {
        var timeAfterStart = (float)DateTime.Now.Subtract(Model.StartTime).TotalSeconds;
        var progressTime = (float)Model.TotalTime.TotalSeconds;

        if (Model.StartTime == DateTime.MaxValue)
        {
            Model.StartTime = DateTime.Now;
        }

        else
        {
            if (timeAfterStart >= progressTime)
            {
                var multiplier = 1;
                if (Model.IsManagerActivated)
                {
                    multiplier = (int)(timeAfterStart / progressTime);
                    Model.StartTime = DateTime.Now.Subtract(TimeSpan.FromSeconds(timeAfterStart % progressTime));
                }

                else
                {
                    Model.State = CellState.Active;
                }

                var profit = new MoneyModel(multiplier * Model.Profit.Number, Model.Profit.Value);
                InProgressEndEvent?.Invoke(profit);
                GetOfflineProfitEvent?.Invoke(profit);
            }
        }
    }

    private void GetInProgressCallBack()
    {
        if (Model.State == CellState.InProgress)
        {
            InProgressEndEvent?.Invoke(Model.Profit);
            if (Model.IsManagerActivated)
            {
                GetInProgressCallBack();
                return;
            }

            Model.State = CellState.Active;
        }
    }

    private void SetParameters(BoardData cellData)
    {
        SetTime(cellData);
        SetProfit(cellData);
    }

    private void SetTime(BoardData cellData)
    {
        if (Model.Level < 1)
        {
            Model.TotalTime = TimeSpan.Zero;
            return;
        }

        Model.TotalTime = cellData.ProcessTime[Model.Level - 1].ToTimeSpan();
    }

    private void SetProfit(BoardData cellData)
    {
        if (Model.Level < 1)
        {
            Model.Profit = null;
            return;
        }

        var profitMultiplier = Model.ActivatedUpgradeLevel + 1;
        var baseProfit = cellData.BaseProfit[Model.Level - 1];
        Model.Profit = new MoneyModel(baseProfit.Number * profitMultiplier, baseProfit.Value);
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

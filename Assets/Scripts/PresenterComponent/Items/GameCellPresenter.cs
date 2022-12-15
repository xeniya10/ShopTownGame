using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public class GameCellPresenter : ButtonSubscription, IGameCell
{
    private readonly GameCellModel _model;
    private readonly IGameCellView _view;

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
        _model.State = state;
        SetParameters(cellData);
        switch (state)
        {
            case CellState.Lock or CellState.Unlock:
                _model.SetDefaultData(cellData.DefaultCell);
                _model.State = state;
                break;

            case CellState.InProgress:
                CalculateStartTime();
                callBack = GetInProgressCallBack;
                break;
        }

        ChangeEvent?.Invoke();
        _view.StartAnimation(_model, callBack);
    }

    public void LevelUp(BoardData cellData)
    {
        _model.Level += 1;
        SetState(CellState.Active, cellData);
        _view.StartLevelUpAnimation(_model);
    }

    public void InitializeManager(ImprovementModel manager, BoardData cellData)
    {
        _model.IsManagerActivated = manager.IsActivated;
        if (_model.IsManagerActivated)
        {
            SetState(CellState.InProgress, cellData);
        }

        _view.InitializeImprovements(_model);
    }

    public void InitializeUpgrade(ImprovementModel upgrade, BoardData cellData)
    {
        _model.ActivatedUpgradeLevel = upgrade.ImprovementLevel - 1;
        if (upgrade.IsActivated)
        {
            _model.ActivatedUpgradeLevel = upgrade.ImprovementLevel;
        }

        _model.AreAllUpgradeLevelsActivated = upgrade.IsActivated;
        SetParameters(cellData);
        _view.InitializeImprovements(_model);
    }

    public void SetCost(int activationNumber, BoardData cellData)
    {
        if (activationNumber > cellData.Cost.Count - 1)
        {
            var lastElement = cellData.Cost[cellData.Cost.Count - 1];
            var costNumber = lastElement.Number * (activationNumber - cellData.Cost.Count + 2);
            _model.Cost = new MoneyModel(costNumber, lastElement.Value);
            return;
        }

        _model.Cost = cellData.Cost[activationNumber];
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
        var timeAfterStart = (float)DateTime.Now.Subtract(_model.StartTime).TotalSeconds;
        var progressTime = (float)_model.TotalTime.TotalSeconds;

        if (_model.StartTime == DateTime.MaxValue)
        {
            _model.StartTime = DateTime.Now;
        }

        else
        {
            if (timeAfterStart >= progressTime)
            {
                var multiplier = 1;
                if (_model.IsManagerActivated)
                {
                    multiplier = (int)(timeAfterStart / progressTime);
                    _model.StartTime = DateTime.Now.Subtract(TimeSpan.FromSeconds(timeAfterStart % progressTime));
                }

                else
                {
                    _model.State = CellState.Active;
                }

                var profit = new MoneyModel(multiplier * _model.Profit.Number, _model.Profit.Value);
                InProgressEndEvent?.Invoke(profit);
                GetOfflineProfitEvent?.Invoke(profit);
            }
        }
    }

    private void GetInProgressCallBack()
    {
        if (_model.State == CellState.InProgress)
        {
            InProgressEndEvent?.Invoke(_model.Profit);
            if (_model.IsManagerActivated)
            {
                GetInProgressCallBack();
                return;
            }

            _model.State = CellState.Active;
        }
    }

    private void SetParameters(BoardData cellData)
    {
        SetTime(cellData);
        SetProfit(cellData);
    }

    private void SetTime(BoardData cellData)
    {
        if (_model.Level < 1)
        {
            _model.TotalTime = TimeSpan.Zero;
            return;
        }

        _model.TotalTime = cellData.ProcessTime[_model.Level - 1].ToTimeSpan();
    }

    private void SetProfit(BoardData cellData)
    {
        if (_model.Level < 1)
        {
            _model.Profit = null;
            return;
        }

        var profitMultiplier = _model.ActivatedUpgradeLevel + 1;
        var baseProfit = cellData.BaseProfit[_model.Level - 1];
        _model.Profit = new MoneyModel(baseProfit.Number * profitMultiplier, baseProfit.Value);
    }

    public bool IsNeighborOf(GameCellPresenter cell)
    {
        var xDiff = Math.Abs(cell.Model.GridIndex[0] - _model.GridIndex[0]);
        var yDiff = Math.Abs(cell.Model.GridIndex[1] - _model.GridIndex[1]);

        if (xDiff == 1 && yDiff == 0 || xDiff == 0 && yDiff == 1)
        {
            return true;
        }

        return false;
    }

    public bool HasSameLevelAs(GameCellPresenter otherCell)
    {
        if (_model.Level == otherCell.Model.Level)
        {
            return true;
        }

        return false;
    }
}
}

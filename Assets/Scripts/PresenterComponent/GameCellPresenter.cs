using System;
using UnityEngine;
using ShopTown.ModelComponent;

namespace ShopTown.PresenterComponent
{
public class GameCellPresenter
{
    private readonly GameCellView _cellView;
    private readonly GameCellModel _cellModel;

    public int Level { get { return _cellModel.Level; } }
    public MoneyModel Cost { get { return _cellModel.Cost; } }
    public MoneyModel Profit { get { return _cellModel.Profit; } }
    public CellState State { get { return _cellModel.State; } }

    private GameCellPresenter(GameCellView cellView, GameCellModel gameModel)
    {
        _cellView = cellView;
        _cellModel = gameModel;
    }

    public GameCellPresenter Create(Transform parent, GameCellModel model)
    {
        _cellView.SetSize(model.Size);
        var view = _cellView.Create(parent);

        if (model.BackgroundNumber < 0)
        {
            var spriteNumber = view.RandomBackgroundNumber();
            model.BackgroundNumber = spriteNumber;
        }

        view.Initialize(model);
        var presenter = new GameCellPresenter(view, model);
        return presenter;
    }

    public void SubscribeToBuyButton(Action<GameCellPresenter> callBack)
    {
        _cellView.BuyButton.onClick.AddListener(() => callBack?.Invoke(this));
    }

    public void SubscribeToClick(Action<GameCellPresenter> callBack)
    {
        _cellView.CellButton.onClick.AddListener(() => callBack?.Invoke(this));
    }

    public void SetActiveSelector(bool isActive)
    {
        _cellView._selectorImage.gameObject.SetActive(isActive);
    }

    public void ManagerUnlock()
    {
        _cellModel.IsUnlockedManager = true;
        _cellView.SetActiveImprovements(_cellModel);
    }

    public void UpgradeUp()
    {
        _cellModel.UpgradeLevel += 1;
        _cellView.SetActiveImprovements(_cellModel);
    }

    public void LevelUp()
    {
        _cellView.HideBusiness(() =>
        {
            _cellModel.Level += 1;
            _cellView.Initialize(_cellModel);
            _cellView.SetActiveState();
        });
    }

    public void Lock()
    {
        _cellModel.Reset();
        _cellModel.State = CellState.Lock;
        _cellView.SetLockState();
    }

    // Cost changes depending on number of activated cells.
    public void Unlock(int activationNumber)
    {
        _cellModel.SetCost(activationNumber);
        _cellView.SetCost(_cellModel.Cost);
        _cellModel.State = CellState.Unlock;
        _cellView.SetUnlockState();
    }

    public void Activate()
    {
        _cellModel.State = CellState.Active;
        _cellModel.ActivatingDate = DateTime.Now;
        _cellView.SetActiveState();
        _cellView.Initialize(_cellModel);
    }

    public void GetInProgress(Action callBack)
    {
        _cellModel.State = CellState.InProgress;
        _cellView.SetInProgressState(_cellModel.TotalTime.TotalSeconds, () =>
        {
            callBack?.Invoke();
            if (_cellModel.IsUnlockedManager == true)
            {
                GetInProgress(callBack);
            }
        });
    }

    public bool IsNeighborOf(GameCellPresenter cell)
    {
        var thisPosition = _cellModel.GridIndex;
        var unlockedPosition = cell._cellModel.GridIndex;
        var xDiff = Math.Abs((int)(unlockedPosition.x - thisPosition.x));
        var yDiff = Math.Abs((int)(unlockedPosition.y - thisPosition.y));

        if (xDiff == 1 && yDiff == 0 || xDiff == 0 && yDiff == 1)
        {
            return true;
        }

        return false;
    }

    public bool HasSameLevelAs(GameCellPresenter otherCell)
    {
        var oneCellLevel = _cellModel.Level;
        var otherCellLevel = otherCell._cellModel.Level;

        if (oneCellLevel == otherCellLevel)
        {
            return true;
        }

        return false;
    }

    public bool IsActivatedEarlierThen(GameCellPresenter otherCell)
    {
        var oneCellActivatingTime = _cellModel.ActivatingDate;
        var otherCellActivatingTime = otherCell._cellModel.ActivatingDate;

        var result = DateTime.Compare(oneCellActivatingTime, otherCellActivatingTime);
        if (result < 0)
        {
            return true;
        }

        return false;
    }
}
}

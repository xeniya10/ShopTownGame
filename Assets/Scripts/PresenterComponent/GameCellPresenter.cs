using System;
using UnityEngine;
using ShopTown.ModelComponent;

namespace ShopTown.PresenterComponent
{
public class GameCellPresenter
{
    private readonly GameCellView _cellView;
    public readonly GameCellModel CellModel;

    private GameCellPresenter(GameCellView cellView, GameCellModel cellModel)
    {
        _cellView = cellView;
        CellModel = cellModel;
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
    //
    // private void SetState(CellState state)
    // {
    //     switch (state)
    //     {
    //         case CellState.Lock:
    //             Lock();
    //             break;
    //
    //         case CellState.Unlock:
    //             Unlock();
    //             break;
    //
    //         case CellState.Active:
    //             Activate();
    //             break;
    //
    //         case CellState.InProgress:
    //             GetInProgress(CellModel.TotalTime,);
    //             break;
    //     }
    // }

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

    public void SetActiveManager(ManagerRowModel manager)
    {
        CellModel.IsActivatedManager = manager.IsActivated;
        _cellView.SetActiveImprovements(CellModel);
    }

    public void SetActiveUpgrade(UpgradeRowModel upgrade)
    {
        if (upgrade.IsActivated)
        {
            CellModel.UpgradeLevel = upgrade.UpgradeLevel;
        }
        else
        {
            CellModel.UpgradeLevel = 0;
        }

        _cellView.SetActiveImprovements(CellModel);
    }

    public void LevelUp(ManagerRowModel manager, UpgradeRowModel upgrade)
    {
        CellModel.Level += 1;
        if (CellModel.State == CellState.InProgress)
        {
            _cellView.StopInProgressAnimation();
        }

        _cellView.HideBusiness(() =>
        {
            _cellView.Initialize(CellModel);
            Activate(null);
        });

        SetActiveManager(manager);
        SetActiveUpgrade(upgrade);
    }

    public void Lock()
    {
        CellModel.State = CellState.Lock;
        CellModel.Reset();
        _cellView.SetLockState();
    }

    // Cost changes depending on number of activated cells.
    public void Unlock(int activationNumber)
    {
        CellModel.State = CellState.Unlock;
        CellModel.Reset();
        CellModel.SetCost(activationNumber);
        _cellView.SetCost(CellModel.Cost);
        _cellView.SetUnlockState();
        _cellView.StopInProgressAnimation();
    }

    public void Activate(Action callBack)
    {
        CellModel.State = CellState.Active;
        CellModel.ActivatingDate = DateTime.Now;
        _cellView.SetActiveState(callBack);
        _cellView.Initialize(CellModel);
    }

    public void GetInProgress(Action callBack)
    {
        CellModel.State = CellState.InProgress;
        _cellView.SetInProgressState(CellModel.TotalTime.TotalSeconds, () =>
        {
            callBack?.Invoke();
            if (CellModel.IsActivatedManager)
            {
                GetInProgress(callBack);
            }
        });
    }

    public bool IsNeighborOf(GameCellPresenter cell)
    {
        var thisPosition = CellModel.GridIndex;
        var unlockedPosition = cell.CellModel.GridIndex;
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
        var oneCellLevel = CellModel.Level;
        var otherCellLevel = otherCell.CellModel.Level;

        if (oneCellLevel == otherCellLevel)
        {
            return true;
        }

        return false;
    }

    public bool IsActivatedEarlierThen(GameCellPresenter otherCell)
    {
        var oneCellActivatingTime = CellModel.ActivatingDate;
        var otherCellActivatingTime = otherCell.CellModel.ActivatingDate;

        var result = DateTime.Compare(oneCellActivatingTime, otherCellActivatingTime);
        if (result < 0)
        {
            return true;
        }

        return false;
    }
}
}

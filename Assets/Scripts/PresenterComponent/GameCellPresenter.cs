using System;
using ShopTown.ModelComponent;
using UnityEngine;

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

        view.StartAnimation(model, null);
        view.SetActiveImprovements(model);
        var presenter = new GameCellPresenter(view, model);
        return presenter;
    }

    public void SetState(CellState state, Action callBack)
    {
        CellModel.SetState(state);
        if (state == CellState.InProgress)
        {
            GetInProgress(callBack);
            return;
        }

        _cellView.StartAnimation(CellModel, callBack);
    }

    // public void Lock()
    // {
    //     CellModel.SetState(CellState.Lock);
    //     _cellView.StartAnimation(CellModel, null);
    // }
    //
    // public void Unlock()
    // {
    //     CellModel.SetState(CellState.Active);
    //     _cellView.StartAnimation(CellModel, null);
    // }
    //
    // public void Activate(Action callBack)
    // {
    //     CellModel.SetState(CellState.Active);
    //     _cellView.StartAnimation(CellModel, callBack);
    // }
    //
    private void GetInProgress(Action callBack)
    {
        _cellView.StartAnimation(CellModel, () =>
        {
            callBack?.Invoke();
            if (CellModel.IsManagerActivated)
            {
                GetInProgress(callBack);
                return;
            }

            CellModel.SetState(CellState.Active);
        });
    }

    public void LevelUp()
    {
        CellModel.Level += 1;
        SetState(CellState.Active, null);
    }

    public void InitializeManager(ManagerRowModel manager)
    {
        CellModel.IsManagerActivated = manager.IsActivated;
        _cellView.SetActiveImprovements(CellModel);
    }

    public void InitializeUpgrade(UpgradeRowModel upgrade)
    {
        if (upgrade.AreAllLevelsActivated)
        {
            CellModel.ActivatedUpgradeLevel = upgrade.UpgradeLevel;
        }
        else
        {
            CellModel.ActivatedUpgradeLevel = upgrade.UpgradeLevel - 1;
        }

        CellModel.AreAllUpgradeLevelsActivated = upgrade.AreAllLevelsActivated;
        _cellView.SetActiveImprovements(CellModel);
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

    public bool IsNeighborOf(GameCellPresenter cell)
    {
        var thisPosition = CellModel.GridIndex;
        var unlockedPosition = cell.CellModel.GridIndex;
        // var xDiff = Math.Abs((int)(unlockedPosition.x - thisPosition.x));
        // var yDiff = Math.Abs((int)(unlockedPosition.y - thisPosition.y));
        var xDiff = Math.Abs((int)(unlockedPosition[0] - thisPosition[0]));
        var yDiff = Math.Abs((int)(unlockedPosition[1] - thisPosition[1]));

        if (xDiff == 1 && yDiff == 0 || xDiff == 0 && yDiff == 1)
        {
            return true;
        }

        return false;
    }

    public bool HasSameLevelAs(GameCellPresenter otherCell)
    {
        if (CellModel.Level == otherCell.CellModel.Level)
        {
            return true;
        }

        return false;
    }
}
}

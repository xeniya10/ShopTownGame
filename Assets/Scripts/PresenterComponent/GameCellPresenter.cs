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
        var presenter = new GameCellPresenter(view, model);
        return presenter;
    }

    public void Lock()
    {
        CellModel.Lock();
        _cellView.StartAnimation(CellModel, null);
    }

    public void Unlock()
    {
        CellModel.Unlock();
        _cellView.StartAnimation(CellModel, null);
    }

    public void Activate(Action callBack)
    {
        CellModel.Activate();
        _cellView.StartAnimation(CellModel, callBack);
    }

    public void GetInProgress(Action callBack)
    {
        CellModel.State = CellState.InProgress;
        _cellView.StartAnimation(CellModel, () =>
        {
            callBack?.Invoke();
            if (CellModel.IsManagerActivated)
            {
                GetInProgress(callBack);
            }
        });
    }

    public void LevelUp()
    {
        CellModel.LevelUp();
        _cellView.StartLevelUpAnimation(CellModel, null);
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
        if (CellModel.Level == otherCell.CellModel.Level)
        {
            return true;
        }

        return false;
    }
}
}

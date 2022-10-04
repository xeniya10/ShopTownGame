using System;
using UnityEngine;
using ShopTown.ModelComponent;

namespace ShopTown.PresenterComponent
{
public class GameCellPresenter
{
    private readonly GameCellView _cellView;
    private readonly GameCellModel _cellModel;

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

        view.Initialize(model.Level, model.BackgroundNumber, model.Position, model.Cost, model.State);
        view.Click(() => view.SetProcessState(model.TotalTime.TotalSeconds));

        var presenter = new GameCellPresenter(view, model);
        return presenter;
    }

    public void Buy(Action callBack)
    {
        _cellView.BuyButton.onClick.AddListener(() => callBack?.Invoke());
    }

    // public void Click(Action callBack)
    // {
    //     _cellView.CellButton.onClick.AddListener(() => callBack?.Invoke());
    // }

    // public void Click()
    // {
    //     _cellView.CellButton.onClick.AddListener(() => _cellView.SetProcessState(_cellModel.TimerInSeconds));
    // }

    // Cost changes depending on number of activated cells.
    public void SetCost(int activationNumber)
    {
        _cellModel.SetCost(activationNumber);
        _cellView.SetCost(_cellModel.Cost);
    }

    public double GetCost()
    {
        return _cellModel.Cost;
    }

    public void Lock()
    {
        _cellModel.State = CellState.Lock;
        _cellView.SetLockState();
    }

    public void Unlock()
    {
        _cellModel.State = CellState.Unlock;
        _cellView.SetUnlockState();
    }

    public void Activate()
    {
        _cellModel.State = CellState.Active;
        _cellModel.ActivatingDate = DateTime.Now;
        _cellView.SetActiveState();
    }

    public Vector2 GetPosition()
    {
        return _cellModel.GridIndex;
    }

    public bool IsNeighbor(Vector2 unlockedPosition)
    {
        var thisPosition = _cellModel.GridIndex;
        var xDiff = Math.Abs((int)(unlockedPosition.x - thisPosition.x));
        var yDiff = Math.Abs((int)(unlockedPosition.y - thisPosition.y));

        if (xDiff == 1 && yDiff == 0 || xDiff == 0 && yDiff == 1)
        {
            if (_cellModel.State == CellState.Lock)
            {
                return true;
            }
        }

        return false;
    }

    public void ChangeProgressBar(float currentTime, float totalTime)
    {}

    public void Merge()
    {}
}
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class GameCellPresenter
{
    private readonly GameCellView _cellView;
    private readonly GameCellModel _cellModel;

    public Action ClickEvent;
    public Action BuyEvent;

    public GameCellPresenter(GameCellView cellView,
    GameCellModel gameModel)
    {
        _cellView = cellView;
        _cellModel = gameModel;
    }

    public GameCellPresenter Create(Transform parent, GameCellModel _cellModel)
    {
        var spriteNumber = _cellView.RandomBackgroundSpriteNumber();
        _cellModel.BackgroundNumber = spriteNumber;
        var cellView = _cellView.Create(parent, _cellModel.CellSize, _cellModel.Position, _cellModel.BackgroundNumber);
        cellView.SetBackgroundSprite(spriteNumber);
        cellView.SetBusinessSprite(_cellModel.Level);
        cellView.ChangeState(_cellModel.State);
        cellView.SetCost(_cellModel.Cost);
        cellView.Click(() => cellView.SetProcessState(_cellModel.ProcessTime.TotalSeconds));

        var cellPresenter = new GameCellPresenter(cellView, _cellModel);
        return cellPresenter;
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

    public void SetCost(int counter)
    {
        _cellModel.SetCost(counter);
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
        return _cellModel.GridIndexes;
    }

    public bool IsNeighbor(Vector2 unlockedPosition)
    {
        var thisPosition = _cellModel.GridIndexes;
        var xDiff = unlockedPosition.x - thisPosition.x;
        var yDiff = unlockedPosition.y - thisPosition.y;

        if (Math.Abs(xDiff) == 1 && Math.Abs(yDiff) == 0 ||
            Math.Abs(xDiff) == 0 && Math.Abs(yDiff) == 1)
        {
            if (_cellModel.State == CellState.Lock)
            {
                return true;
            }
        }

        return false;
    }

    public void ChangeProgressBar(float currentTime, float totalTime)
    {

    }

    public void Merge() { }


}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.ViewComponent;
using UnityEngine;
using VContainer;

namespace ShopTown.ControllerComponent
{
public class GameBoardController : StorageManager
{
    private GameDataModel _data;

    [Inject] private readonly GameScreenView _gameScreen;
    [Inject] private readonly GameCellView _view;
    [Inject] private readonly GameBoardModel _gameBoardModel;
    [Inject] private readonly GameCellData _cellData;

    private string _key = "GameBoard";

    private List<GameCellModel> _models = new List<GameCellModel>();
    private List<GameCellPresenter> _gameCells = new List<GameCellPresenter>();
    private List<GameCellPresenter> _selectedCells = new List<GameCellPresenter>();

    public Action<GameCellPresenter> CellActivateEvent;
    public Action<int, bool> CellUnlockEvent;

    public void Initialize(ref GameDataModel data)
    {
        // PlayerPrefs.DeleteKey(_key);
        _data = data;
        _models = JsonConvert.DeserializeObject<List<GameCellModel>>(Load(_key));

        if (_models == null)
        {
            CreateDefaultModels();
        }

        CreateBoard();
    }

    private void CreateDefaultModels()
    {
        _models = new List<GameCellModel>();

        for (var i = 0; i < _gameBoardModel.Rows; i++)
        {
            for (var j = 0; j < _gameBoardModel.Columns; j++)
            {
                var cell = new GameCellModel();
                cell.Initialize(0, DateTime.MaxValue);
                cell.BackgroundNumber = int.MinValue;
                cell.SetState(CellState.Lock);
                cell.SetGridIndex(i, j);
                cell.Size = _gameBoardModel.CalculateCellSize();
                cell.Position = _gameBoardModel.CalculateCellPosition(j, i, cell.Size);
                _models.Add(cell);

                if (i == _gameBoardModel.Rows - 2 && j == _gameBoardModel.Columns - 2)
                {
                    cell.Level = _data.MinLevel;
                    cell.SetState(CellState.Unlock);
                }
            }
        }

        SaveData();
    }

    private void CreateBoard()
    {
        foreach (var model in _models)
        {
            var view = _view.Create(_gameScreen.GameBoard, model.Size);
            var cell = new GameCellPresenter(view, model, _cellData);
            cell.Initialize();
            if (model.Cost == null && model.State == CellState.Unlock)
            {
                cell.SetCost(_data.ActivationNumber);
            }

            cell.ModelChangeEvent += () => SaveData();
            cell.InProgressAnimationEndEvent += (profit) => _data.AddToBalance(profit);
            cell.SubscribeToBuyButton(TryBuy);
            cell.SubscribeToClick(Select);
            _gameCells.Add(cell);
        }
    }

    private void TryBuy(GameCellPresenter cell)
    {
        if (_data.CanBuy(cell.Model.Cost))
        {
            cell.Model.Level = _data.MinLevel;
            cell.SetState(CellState.Active);
            UnlockNeighbors(cell);
            CellActivateEvent?.Invoke(cell);
        }
    }

    private void Select(GameCellPresenter selectedCell)
    {
        selectedCell.SetActiveSelector(true);
        _selectedCells.Add(selectedCell);

        if (_selectedCells.Count < 2)
        {
            return;
        }

        var oneCell = _selectedCells[1];
        var otherCell = _selectedCells[0];

        if (oneCell.Equals(otherCell) && oneCell.Model.State == CellState.Active)
        {
            selectedCell.SetState(CellState.InProgress);
        }

        if (oneCell.IsNeighborOf(otherCell) && oneCell.HasSameLevelAs(otherCell))
        {
            Merge(oneCell, otherCell);
        }

        _selectedCells.ForEach(cell => cell.SetActiveSelector(false));
        _selectedCells.Clear();
    }

    private void Merge(GameCellPresenter oneCell, GameCellPresenter otherCell)
    {
        if (oneCell.Model.Level < _data.MaxLevel)
        {
            _data.SetActivationNumber(_data.ActivationNumber + 1);
            oneCell.LevelUp();
            otherCell.SetCost(_data.ActivationNumber);
            otherCell.SetState(CellState.Unlock);
            CellActivateEvent?.Invoke(oneCell);
            CellUnlockEvent?.Invoke(oneCell.Model.Level - 1, HasCallWithLevel(oneCell.Model.Level - 1));
        }
    }

    private void UnlockNeighbors(GameCellPresenter activatedCell)
    {
        var neighbors =
            _gameCells.FindAll(cell => cell.IsNeighborOf(activatedCell) && cell.Model.State == CellState.Lock);

        if (neighbors.Count > 0)
        {
            _data.SetActivationNumber(_data.ActivationNumber + 1);
            neighbors.ForEach(cell => cell.SetCost(_data.ActivationNumber));
            neighbors.ForEach(cell => cell.SetState(CellState.Unlock));
        }
    }

    private bool HasCallWithLevel(int level)
    {
        if (FindCell(level) == null)
        {
            return false;
        }

        return true;
    }

    public void SaveData()
    {
        Save(_key, _models);
    }

    public void InitializeManager(ImprovementModel improvement)
    {
        FindAllCells(improvement.Level).ForEach(cell => cell.InitializeManager(improvement));
    }

    public void InitializeUpgrade(ImprovementModel improvement)
    {
        FindAllCells(improvement.Level).ForEach(cell => cell.InitializeUpgrade(improvement));
    }

    private GameCellPresenter FindCell(int level)
    {
        return _gameCells.Find(cell => cell.Model.Level == level);
    }

    private List<GameCellPresenter> FindAllCells(int level)
    {
        return _gameCells.FindAll(cell => cell.Model.Level == level);
    }
}
}

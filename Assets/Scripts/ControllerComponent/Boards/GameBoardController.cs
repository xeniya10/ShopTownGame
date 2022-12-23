using System;
using System.Collections.Generic;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.ControllerComponent
{
public class GameBoardController : IGameBoardController
{
    [Inject] private readonly IGameData _data;
    [Inject] private readonly IStorageManager _storage;
    [Inject] private readonly IBoard _board;
    [Inject] private readonly IPresenterFactory<IGameCell> _presenterFactory;
    [Inject] private readonly IGameCellView _view;
    [Inject] private readonly IButtonSubscriber _subscriber;
    [Inject] private readonly IBoardData _defaultData;

    private string _key = "GameBoard";

    private List<GameCellModel> _models = new List<GameCellModel>();
    private List<IGameCell> _presenters = new List<IGameCell>();
    private List<IGameCell> _selectedCells = new List<IGameCell>();

    public event Action<GameCellModel> ActivateEvent;
    public event Action<int, bool> UnlockEvent;
    public event Action<IGameData> SetOfflineProfitEvent;

    public void Initialize()
    {
        _storage.Load(ref _models, _key);
        CreateBoard();
    }

    private void CreateDefaultModels()
    {
        _models = new List<GameCellModel>();

        for (var i = 0; i < _defaultData.GetDefaultBoard().Rows; i++)
        {
            for (var j = 0; j < _defaultData.GetDefaultBoard().Columns; j++)
            {
                var cell = new GameCellModel();
                cell.SetDefaultData(_defaultData.GetDefaultCell());
                cell.GridIndex = new[] {j, i};
                cell.Size = _defaultData.GetDefaultBoard().CalculateCellSize();
                cell.Position = _defaultData.GetDefaultBoard().CalculateCellPosition(j, i);
                _models.Add(cell);

                if (i == _defaultData.GetDefaultBoard().Rows - 2 && j == _defaultData.GetDefaultBoard().Columns - 2)
                {
                    cell.Level = _data.GameData.MinLevel;
                    cell.State = CellState.Unlock;
                }
            }
        }

        _storage.Save(_key, _models);
    }

    private void CreateBoard()
    {
        if (_models == null)
        {
            CreateDefaultModels();
        }

        foreach (var model in _models)
        {
            var view = _view.Instantiate(_board.GetGameBoard());
            var presenter = _presenterFactory.Create(model, view);
            presenter.ChangeEvent += () => _storage.Save(_key, _models);
            presenter.InProgressEndEvent += (profit) => _data.GameData.AddToBalance(profit);
            presenter.GetOfflineProfitEvent += (profit) => _data.GameData.AddToOfflineBalance(profit);
            presenter.SubscribeToBuyButton(_subscriber, TryBuy);
            presenter.SubscribeToCellClick(_subscriber, Select);
            presenter.SetState(model.State, _defaultData);

            if (model.Cost == null && model.State == CellState.Unlock)
            {
                presenter.SetCost(_data.GameData.ActivationNumber, _defaultData);
            }

            _presenters.Add(presenter);
        }

        SetOfflineProfitEvent?.Invoke(_data);
    }

    private void TryBuy(IGameCell cell)
    {
        if (_data.GameData.CanBuy(cell.Model.Cost))
        {
            cell.Model.Level = _data.GameData.MinLevel;
            cell.SetState(CellState.Active, _defaultData);
            UnlockNeighbors(cell);
            ActivateEvent?.Invoke(cell.Model);
            UnlockEvent?.Invoke(cell.Model.Level, IsCellWithLevel(cell.Model.Level));
        }
    }

    private void Select(IGameCell selectedCell)
    {
        selectedCell.SetActiveSelector(true);
        _selectedCells.Add(selectedCell);

        if (_selectedCells.Count < 2)
        {
            return;
        }

        var oneCell = _selectedCells[1];
        var anotherCell = _selectedCells[0];

        if (oneCell.Equals(anotherCell) && oneCell.Model.State == CellState.Active)
        {
            selectedCell.Model.StartTime = DateTime.MaxValue;
            selectedCell.SetState(CellState.InProgress, _defaultData);
        }

        if (oneCell.IsNeighborOf(anotherCell) && oneCell.HasSameLevelAs(anotherCell))
        {
            Merge(oneCell, anotherCell);
        }

        _selectedCells.ForEach(cell => cell.SetActiveSelector(false));
        _selectedCells.Clear();
    }

    private void Merge(IGameCell oneCell, IGameCell anotherCell)
    {
        if (oneCell.Model.Level < _data.GameData.MaxLevel)
        {
            _data.GameData.SetActivationNumber(_data.GameData.ActivationNumber + 1);
            oneCell.LevelUp(_defaultData);
            anotherCell.SetCost(_data.GameData.ActivationNumber, _defaultData);
            anotherCell.SetState(CellState.Unlock, _defaultData);
            ActivateEvent?.Invoke(oneCell.Model);
            UnlockEvent?.Invoke(oneCell.Model.Level - 1, IsCellWithLevel(oneCell.Model.Level - 1));
            UnlockEvent?.Invoke(oneCell.Model.Level, IsCellWithLevel(oneCell.Model.Level));
        }
    }

    private void UnlockNeighbors(IGameCell activatedCell)
    {
        var neighbors =
            _presenters.FindAll(cell => cell.IsNeighborOf(activatedCell) && cell.Model.State == CellState.Lock);

        if (neighbors.Count > 0)
        {
            _data.GameData.SetActivationNumber(_data.GameData.ActivationNumber + 1);
            neighbors.ForEach(cell => cell.SetCost(_data.GameData.ActivationNumber, _defaultData));
            neighbors.ForEach(cell => cell.SetState(CellState.Unlock, _defaultData));
        }
    }

    private bool IsCellWithLevel(int level)
    {
        if (FindCell(level) == null)
        {
            return false;
        }

        return true;
    }

    public void Dispose()
    {
        _storage.Save(_key, _models);
    }

    public void InitializeManager(ImprovementModel improvement)
    {
        FindAllCells(improvement.Level).ForEach(cell => cell.InitializeManager(improvement, _defaultData));
    }

    public void InitializeUpgrade(ImprovementModel improvement)
    {
        FindAllCells(improvement.Level).ForEach(cell => cell.InitializeUpgrade(improvement, _defaultData));
    }

    private IGameCell FindCell(int level)
    {
        return _presenters.Find(cell => cell.Model.Level == level);
    }

    private List<IGameCell> FindAllCells(int level)
    {
        return _presenters.FindAll(cell => cell.Model.Level == level);
    }
}
}

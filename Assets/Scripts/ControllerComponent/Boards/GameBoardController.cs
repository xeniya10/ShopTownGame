using System;
using System.Collections.Generic;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.ControllerComponent
{
public class GameBoardController : StorageManager, IGameBoardController, IDisposable
{
    [Inject] private readonly IGameData _data;
    [Inject] private readonly IInitializable<MoneyModel> _welcome;
    [Inject] private readonly IShowable<GameCellModel> _profile;
    [Inject] private readonly IBoard _board;
    [Inject] private readonly IGameCellView _view;
    [Inject] private readonly BoardData _defaultData;

    private string _key = "GameBoard";
    private MoneyModel _offlineProfit;

    private List<GameCellModel> _models = new List<GameCellModel>();
    private List<GameCellPresenter> _presenters = new List<GameCellPresenter>();
    private List<GameCellPresenter> _selectedCells = new List<GameCellPresenter>();

    public event Action<GameCellModel> ActivateEvent;
    public event Action<int, bool> UnlockEvent;

    public void Initialize()
    {
        DeleteKey(_key);
        SetData(ref _models, _key, CreateDefaultModels);
        CreateBoard();
    }

    private void CreateDefaultModels()
    {
        _models = new List<GameCellModel>();

        for (var i = 0; i < _defaultData.DefaultBoard.Rows; i++)
        {
            for (var j = 0; j < _defaultData.DefaultBoard.Columns; j++)
            {
                var cell = new GameCellModel();
                cell.SetDefaultData(_defaultData.DefaultCell);
                cell.GridIndex = new[] {j, i};
                cell.Size = _defaultData.DefaultBoard.CalculateCellSize();
                cell.Position = _defaultData.DefaultBoard.CalculateCellPosition(j, i);
                _models.Add(cell);

                if (i == _defaultData.DefaultBoard.Rows - 2 && j == _defaultData.DefaultBoard.Columns - 2)
                {
                    cell.Level = _data.GameData.MinLevel;
                    cell.State = CellState.Unlock;
                }
            }
        }

        Save(_key, _models);
    }

    private void CreateBoard()
    {
        foreach (var model in _models)
        {
            var view = _view.Instantiate(_board.GetGameBoard());
            var cell = new GameCellPresenter(view, model);
            cell.ChangeEvent += () => Save(_key, _models);
            cell.InProgressEndEvent += (profit) => _data.GameData.AddToBalance(profit);
            cell.WelcomeBackEvent += AddOfflineProfit;
            cell.SubscribeToBuyButton(TryBuy);
            cell.SubscribeToCellClick(Select);
            cell.Initialize(_defaultData);

            if (model.Cost == null && model.State == CellState.Unlock)
            {
                cell.SetCost(_data.GameData.ActivationNumber, _defaultData);
            }

            _presenters.Add(cell);
        }

        if (_offlineProfit != null)
        {
            _welcome.Initialize(_offlineProfit);
        }
    }

    private void TryBuy(GameCellPresenter cell)
    {
        if (_data.GameData.CanBuy(cell.Model.Cost))
        {
            cell.Model.Level = _data.GameData.MinLevel;
            cell.SetState(CellState.Active, _defaultData);
            UnlockNeighbors(cell);
            ShowLevelProfile(cell.Model);
            ActivateEvent?.Invoke(cell.Model);
            UnlockEvent?.Invoke(cell.Model.Level, HasCellWithLevel(cell.Model.Level));
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
            selectedCell.Model.StartTime = DateTime.MaxValue;
            selectedCell.SetState(CellState.InProgress, _defaultData);
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
        if (oneCell.Model.Level < _data.GameData.MaxLevel)
        {
            _data.GameData.SetActivationNumber(_data.GameData.ActivationNumber + 1);
            oneCell.LevelUp(_defaultData);
            otherCell.SetCost(_data.GameData.ActivationNumber, _defaultData);
            otherCell.SetState(CellState.Unlock, _defaultData);
            ShowLevelProfile(oneCell.Model);
            ActivateEvent?.Invoke(oneCell.Model);
            UnlockEvent?.Invoke(oneCell.Model.Level - 1, HasCellWithLevel(oneCell.Model.Level - 1));
            UnlockEvent?.Invoke(oneCell.Model.Level, HasCellWithLevel(oneCell.Model.Level));
        }
    }

    private void AddOfflineProfit(MoneyModel profit)
    {
        if (_offlineProfit == null)
        {
            _offlineProfit = new MoneyModel(profit.Number, profit.Value);
            return;
        }

        _offlineProfit.Number += profit.Number;
    }

    private void ShowLevelProfile(GameCellModel model)
    {
        if (model.Level > _data.GameData.MaxOpenedLevel)
        {
            _profile.Show(model);
            _data.GameData.SetMaxOpenedLevel(model.Level);
        }
    }

    private void UnlockNeighbors(GameCellPresenter activatedCell)
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

    private bool HasCellWithLevel(int level)
    {
        if (FindCell(level) == null)
        {
            return false;
        }

        return true;
    }

    public void Dispose()
    {
        Save(_key, _models);
    }

    public void InitializeManager(ImprovementModel improvement)
    {
        FindAllCells(improvement.Level).ForEach(cell => cell.InitializeManager(improvement, _defaultData));
    }

    public void InitializeUpgrade(ImprovementModel improvement)
    {
        FindAllCells(improvement.Level).ForEach(cell => cell.InitializeUpgrade(improvement, _defaultData));
    }

    private GameCellPresenter FindCell(int level)
    {
        return _presenters.Find(cell => cell.Model.Level == level);
    }

    private List<GameCellPresenter> FindAllCells(int level)
    {
        return _presenters.FindAll(cell => cell.Model.Level == level);
    }
}
}

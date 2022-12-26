using System;
using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "GameData", menuName = "GameData")]
public class GameData : ScriptableObject, IDefaultModel<GameDataModel>
{
    [SerializeField] private GameDataModel _defaultGameData = new GameDataModel();

    public GameDataModel GetDefaultModel()
    {
        if (_defaultGameData == null)
        {
            throw new NullReferenceException($"{GetType().Name}.{nameof(GetDefaultModel)}: Object is null");
        }

        return _defaultGameData;
    }
}
}

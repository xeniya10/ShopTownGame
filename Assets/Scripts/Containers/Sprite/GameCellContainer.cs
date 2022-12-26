using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShopTown.SpriteContainer
{
[CreateAssetMenu(fileName = "GameCellSprites", menuName = "GameCellSprites")]
public class GameCellContainer : ScriptableObject, IGameCellSprites
{
    [SerializeField] private List<Sprite> _businessSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> _backgroundSprites = new List<Sprite>();

    public Sprite GetBusinessSprites(int level)
    {
        if (_businessSprites == null || _businessSprites.Count == 0 || level > _businessSprites.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetBusinessSprites)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _businessSprites[level - 1];
    }

    public Sprite GetBackgroundSprites(int number)
    {
        if (_backgroundSprites == null || _backgroundSprites.Count == 0 || number > _backgroundSprites.Count - 1)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetBackgroundSprites)}: List is null/empty or input number: {number} greater, then count of list");
        }

        return _backgroundSprites[number];
    }

    public int GetBackgroundSpritesCount()
    {
        if (_backgroundSprites == null)
        {
            throw new NullReferenceException($"{GetType().Name}.{nameof(GetBackgroundSpritesCount)}: List is null");
        }

        return _backgroundSprites.Count;
    }
}
}

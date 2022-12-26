using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShopTown.SpriteContainer
{
[CreateAssetMenu(fileName = "SplashCellSprites", menuName = "SplashCellSprites")]
public class SplashCellContainer : ScriptableObject, ISplashCellSprites
{
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();

    public Sprite GetSprite(int number)
    {
        if (_sprites == null || _sprites.Count == 0 || number > _sprites.Count - 1)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetSprite)}: List is null/empty or input number: {number} greater, then count of list");
        }

        return _sprites[number];
    }
}
}

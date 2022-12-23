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
        if (_businessSprites.Count == 0 || level > _businessSprites.Count)
        {
            Debug.Log("List of Business Sprites is empty or input level greater, then count of list");
            return null;
        }

        return _businessSprites[level - 1];
    }

    public Sprite GetBackgroundSprites(int number)
    {
        if (_backgroundSprites.Count == 0 || number > _backgroundSprites.Count - 1)
        {
            Debug.Log("List of Background Sprites is empty or input level greater, then count of list");
            return null;
        }

        return _backgroundSprites[number];
    }

    public int GetBackgroundSpritesCount()
    {
        return _backgroundSprites.Count;
    }
}
}

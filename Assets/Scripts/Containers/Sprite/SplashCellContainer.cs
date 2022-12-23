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
        if (_sprites.Count == 0 || number > _sprites.Count - 1)
        {
            Debug.Log("List of Sprites is empty or input level greater, then count of list");
            return null;
        }

        return _sprites[number];
    }
}
}

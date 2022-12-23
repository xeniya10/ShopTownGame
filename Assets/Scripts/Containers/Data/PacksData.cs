using System.Collections.Generic;
using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "PacksData", menuName = "PacksData")]
public class PacksData : ScriptableObject, IPackData
{
    [SerializeField] private List<PackModel> _dollarPacks = new List<PackModel>();
    [SerializeField] private List<PackModel> _goldPacks = new List<PackModel>();

    public int GetDollarPackCount()
    {
        return _dollarPacks.Count;
    }

    public PackModel GetDollarPack(int size)
    {
        if (_dollarPacks.Count == 0 || size > _dollarPacks.Count)
        {
            Debug.Log("List of Dollar Packs is empty or input level greater, then count of list");
            return null;
        }

        return _dollarPacks[size - 1];
    }

    public int GetGoldPackCount()
    {
        return _goldPacks.Count;
    }

    public PackModel GetGoldPack(int size)
    {
        if (_goldPacks.Count == 0 || size > _goldPacks.Count)
        {
            Debug.Log("List of Gold Packs is empty or input level greater, then count of list");
            return null;
        }

        return _goldPacks[size - 1];
    }
}
}

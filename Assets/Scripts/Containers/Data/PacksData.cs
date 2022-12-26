using System;
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
        if (_dollarPacks == null)
        {
            throw new NullReferenceException($"{GetType().Name}.{nameof(GetDollarPackCount)}: List is null");
        }

        return _dollarPacks.Count;
    }

    public PackModel GetDollarPack(int size)
    {
        if (_dollarPacks == null || _dollarPacks.Count == 0 || size > _dollarPacks.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetDollarPack)}: List is null/empty or input size: {size} greater, then count of list");
        }

        return _dollarPacks[size - 1];
    }

    public int GetGoldPackCount()
    {
        if (_goldPacks == null)
        {
            throw new NullReferenceException($"{GetType().Name}.{nameof(GetGoldPackCount)}: List is null");
        }

        return _goldPacks.Count;
    }

    public PackModel GetGoldPack(int size)
    {
        if (_goldPacks == null || _goldPacks.Count == 0 || size > _goldPacks.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetGoldPack)}: List is null/empty or input size: {size} greater, then count of list");
        }

        return _goldPacks[size - 1];
    }
}
}

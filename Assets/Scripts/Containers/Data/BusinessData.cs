using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "BusinessData", menuName = "BusinessData")]
public class BusinessData : ScriptableObject, IBusinessData
{
    [SerializeField] private List<string> _names = new List<string>();

    public string GetName(int level)
    {
        if (_names == null || _names.Count == 0 || level > _names.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetName)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _names[level - 1];
    }
}
}

using System.Collections.Generic;
using UnityEngine;

namespace ShopTown.Data
{
public class BusinessData : ScriptableObject, IBusinessData
{
    [SerializeField] private List<string> _names = new List<string>();

    public string GetName(int level)
    {
        if (_names.Count == 0 || level > _names.Count)
        {
            Debug.Log("List of Names is empty or input level greater, then count of list");
            return null;
        }

        return _names[level - 1];
    }
}
}

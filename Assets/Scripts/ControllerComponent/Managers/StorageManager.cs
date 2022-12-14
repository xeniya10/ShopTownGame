using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ShopTown.ControllerComponent
{
public class StorageManager : IStorageManager
{
    public void Save(string key, object value)
    {
        var json = JsonConvert.SerializeObject(value, Formatting.Indented);
        PlayerPrefs.SetString(key, json);
    }

    public string Load(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public void SetData<T>(ref T variable, string key, Action creationDefaultValue)
    {
        variable = JsonConvert.DeserializeObject<T>(Load(key));

        if (variable == null)
        {
            creationDefaultValue?.Invoke();
        }
    }
}
}

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ShopTown.ControllerComponent
{
public abstract class StorageManager
{
    protected void Save(string key, object value)
    {
        var json = JsonConvert.SerializeObject(value, Formatting.Indented);
        PlayerPrefs.SetString(key, json);
    }

    private string Load(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    protected void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    protected void SetData<T>(ref T variable, string key, Action creationDefaultValue)
    {
        variable = JsonConvert.DeserializeObject<T>(Load(key));

        if (variable == null)
        {
            creationDefaultValue?.Invoke();
        }
    }
}
}

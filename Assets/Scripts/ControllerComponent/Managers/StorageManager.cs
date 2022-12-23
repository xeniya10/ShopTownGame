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

    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public void Load<T>(ref T variable, string key)
    {
        variable = JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key));
    }
}
}

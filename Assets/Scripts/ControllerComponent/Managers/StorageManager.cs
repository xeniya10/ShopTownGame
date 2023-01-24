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
        PlayerPrefs.Save();
    }

    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public T Load<T>(string key)
    {
        return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key));
    }
}
}

using System;

namespace ShopTown.ControllerComponent
{
public interface IStorageManager
{
    public void Save(string key, object value);

    public string Load(string key);

    public void DeleteKey(string key);

    public void SetData<T>(ref T variable, string key, Action creationDefaultValue);
}
}

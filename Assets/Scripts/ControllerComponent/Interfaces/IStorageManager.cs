namespace ShopTown.ControllerComponent
{
public interface IStorageManager
{
    public void Save(string key, object value);

    public void DeleteKey(string key);

    public T Load<T>(string key);
}
}

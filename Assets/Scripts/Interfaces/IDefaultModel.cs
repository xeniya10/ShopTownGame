namespace ShopTown.Data
{
public interface IDefaultModel<out T>
{
    T GetDefaultModel();
}
}

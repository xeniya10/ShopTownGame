using ShopTown.ModelComponent;

namespace ShopTown.Data
{
public interface IPackData
{
    public int GetDollarPackCount();

    public PackModel GetDollarPack(int size);

    public int GetGoldPackCount();

    public PackModel GetGoldPack(int size);
}
}

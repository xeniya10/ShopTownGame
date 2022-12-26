using ShopTown.ModelComponent;

namespace ShopTown.Data
{
public interface IImprovementData : IDefaultModel<ImprovementModel>
{
    string GetBusinessName(int level);

    string GetManagerName(int level);

    MoneyModel GetManagerCost(int level);

    string GetUpgradeNames(int level, int improvementLevel);

    MoneyModel GetUpgradeCost(int level);
}
}

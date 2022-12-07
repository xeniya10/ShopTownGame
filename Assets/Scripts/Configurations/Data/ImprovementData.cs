using System.Collections.Generic;
using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "ImprovementData", menuName = "ImprovementData")]
public class ImprovementData : ScriptableObject
{
    public BusinessData BusinessNames;

    public ImprovementModel DefaultModel = new ImprovementModel();

    [Header("Manager Data")]
    public List<string> ManagerNames = new List<string>();

    public List<MoneyModel> ManagerBaseCost = new List<MoneyModel>();

    [Header("Upgrade Data")]
    public List<string> FirstLevelUpgradeNames = new List<string>();

    public List<string> SecondLevelUpgradeNames = new List<string>();

    public List<string> ThirdLevelUpgradeNames = new List<string>();

    public List<MoneyModel> UpgradeBaseCost = new List<MoneyModel>();
}
}

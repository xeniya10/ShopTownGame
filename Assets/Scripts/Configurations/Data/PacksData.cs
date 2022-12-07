using System.Collections.Generic;
using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "PacksData", menuName = "PacksData")]
public class PacksData : ScriptableObject
{
    public List<PackModel> DollarPacks = new List<PackModel>();

    public List<PackModel> GoldPacks = new List<PackModel>();
}
}

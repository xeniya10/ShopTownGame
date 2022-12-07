using UnityEngine;

namespace ShopTown.ViewComponent
{
public interface IBoard
{
    Transform GetGameBoard();

    Transform GetManagerBoard();

    Transform GetUpgradeBoard();
}
}

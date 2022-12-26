using UnityEngine;

namespace ShopTown.ViewComponent
{
public interface IInstantiatable<T>
{
    T Instantiate(Transform parent);
}
}

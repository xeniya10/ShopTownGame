namespace ShopTown.ViewComponent
{
public interface ICellView<T1, T2> : IView, IInstantiatable<T1>, IInitializable<T2>, IBuyButton
{}

public interface ICellView<T> : IView, IInstantiatable<ICellView<T>>, IInitializable<T>, IBuyButton
{}

public interface IView
{}
}

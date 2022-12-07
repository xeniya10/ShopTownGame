using ShopTown.ModelComponent;

namespace ShopTown.ViewComponent
{
public interface IGameScreenView : IInitializable<GameDataModel>, IBoard, IPurchaseButton, IMenuButton
{}
}

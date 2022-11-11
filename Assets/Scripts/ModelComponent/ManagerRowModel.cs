namespace ShopTown.ModelComponent
{
public class ManagerRowModel : ImprovementModel
{
    public bool IsActivated;

    public void Initialize(int level, bool isActivated = false, ImprovementState state = ImprovementState.Hide)
    {
        Level = level;
        IsActivated = isActivated;
        SetState(state);
    }
}
}

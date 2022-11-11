namespace ShopTown.ModelComponent
{
public class UpgradeRowModel : ImprovementModel
{
    public int UpgradeLevel;
    public bool AreAllLevelsActivated;

    public void Initialize(int level, int upgradeLevel = 1, bool areAllLevelsActivated = false,
        ImprovementState state = ImprovementState.Hide)
    {
        Level = level;
        UpgradeLevel = upgradeLevel;
        AreAllLevelsActivated = areAllLevelsActivated;
        SetState(state);
    }
}
}

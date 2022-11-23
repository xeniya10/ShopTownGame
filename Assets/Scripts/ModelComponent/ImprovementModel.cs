namespace ShopTown.ModelComponent
{
public enum ImprovementState { Hide, Lock, Unlock }

public class ImprovementModel
{
    // Description
    public int Level;
    public int ImprovementLevel;
    public string Description;
    public string Name;

    // Cost
    public MoneyModel Cost;

    // State
    public bool IsActivated;
    public ImprovementState State;

    public void Initialize(int level, int improvementLevel = 1, bool isActivated = false,
        ImprovementState state = ImprovementState.Hide)
    {
        Level = level;
        ImprovementLevel = improvementLevel;
        IsActivated = isActivated;
        SetState(state);
    }

    public void SetState(ImprovementState state)
    {
        State = state;
    }
}

// public class ManagerModel : ImprovementModel
// {
//     // public override void Initialize(int level, int managerLevel = 1, bool isActivated = false,
//     //     ImprovementState state = ImprovementState.Hide)
//     // {
//     //     Level = level;
//     //     ImprovementLevel = managerLevel;
//     //     IsActivated = isActivated;
//     //     SetState(state);
//     // }
// }
//
// public class UpgradeModel : ImprovementModel
// {
//     // public override void Initialize(int level, int upgradeLevel = 1, bool areAllLevelsActivated = false,
//     //     ImprovementState state = ImprovementState.Hide)
//     // {
//     //     Level = level;
//     //     ImprovementLevel = upgradeLevel;
//     //     IsActivated = areAllLevelsActivated;
//     //     SetState(state);
//     // }
// }
}

namespace ShopTown.ModelComponent
{
public enum ImprovementState { Hide, Lock, Unlock }

public abstract class ImprovementModel
{
    // Description
    public int Level;
    public string Description;
    public string Name;

    // Cost
    public MoneyModel Cost;

    // State
    public ImprovementState State;

    public void SetState(ImprovementState state)
    {
        State = state;
    }
}
}

using ShopTown.Data;

namespace ShopTown.ModelComponent
{
public enum ImprovementState { Hide, Lock, Unlock }

public class ImprovementModel
{
    // Description
    public int Level;
    public string Description;
    public string Name;

    // Cost
    public MoneyModel Cost;

    // State
    public ImprovementState State;

    // Data Containers
    private readonly BusinessData _business;

    public ImprovementModel(BusinessData business)
    {
        _business = business;
    }
    
    public void SetState(ImprovementState state)
    {
        State = state;
    }
}
}

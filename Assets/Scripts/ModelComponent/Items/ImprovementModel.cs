using System;

namespace ShopTown.ModelComponent
{
public enum ImprovementState { Hide, Lock, Unlock }

[Serializable]
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

    public void SetDefaultData(ImprovementModel defaultModel)
    {
        ImprovementLevel = defaultModel.ImprovementLevel;
        IsActivated = defaultModel.IsActivated;
        State = defaultModel.State;
    }
}
}

using System;

public class ManagerRowModel
{
    public int Level;
    public string BusinessName;
    public string Name;
    public string Description { get { return String.Format("Hire manager to run your {0}", BusinessName); } }
    public double MoneyCost;
    public double GoldCost;
    public bool Unlocked = false;
}

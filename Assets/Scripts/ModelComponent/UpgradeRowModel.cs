using System;

public class UpgradeRowModel
{
    public int Level;
    public int UpgradeLevel;
    public string BusinessName;
    public string Name;
    public string Description { get { return String.Format("{0} profit x{1}", BusinessName, UpgradeLevel + 1); } }
    public double MoneyCost;
    public double GoldCost;
    public bool Unlocked = false;
}

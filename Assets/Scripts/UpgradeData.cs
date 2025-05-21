using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType { AutoCollector, TapMultiplier, OfflineEarnings }
[CreateAssetMenu(menuName = "IdleClicker/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;
    public string upgradeName;
    public float baseCost;
    public float costMultiplier;
    public float baseValue;

    public float GetCost(int level) => baseCost * Mathf.Pow(costMultiplier, level);
    public float GetValue(int level) => baseValue * level;
}

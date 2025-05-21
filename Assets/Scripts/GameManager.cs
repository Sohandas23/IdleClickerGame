using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float Coins;
    public float TapMultiplier => upgrades[UpgradeType.TapMultiplier];
    public Dictionary<UpgradeType, int> upgrades = new();

    public event Action<float> OnCoinsChanged;
    public List<UpgradeData> upgradeDataList;
    public Dictionary<UpgradeType, UpgradeData> upgradeDefinitions = new();
    private SaveData saveData;
    
    public event Action OnUpgradesLoaded;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        saveData = SaveSystem.Load();
        if (saveData == null)
            saveData = new SaveData(); 

        Coins = saveData.coins;

        foreach (UpgradeType type in System.Enum.GetValues(typeof(UpgradeType)))
        {
            string key = type.ToString();
            if (saveData.upgradeLevels.ContainsKey(key))
                upgrades[type] = saveData.upgradeLevels[key];
            else
                upgrades[type] = type == UpgradeType.TapMultiplier ? 1 : 0;
        }

        upgrades[UpgradeType.TapMultiplier] = 1; 
    }

    public int GetUpgradeLevel(UpgradeType type)
    {
        return upgrades.ContainsKey(type) ? upgrades[type] : 0;
    }

    public void TryUpgrade(UpgradeType type)
    {
        if (!upgradeDefinitions.ContainsKey(type)) return;

        UpgradeData data = upgradeDefinitions[type];
        int currentLevel = GetUpgradeLevel(type);
        float cost = data.GetCost(currentLevel);

        if (Coins >= cost)
        {
            Coins -= cost;
            upgrades[type] = currentLevel + 1;
            OnCoinsChanged?.Invoke(Coins);
        }
    }

    private void Start()
    {
        foreach (var data in upgradeDataList)
        {
            upgradeDefinitions[data.upgradeType] = data;
        }
        saveData = SaveSystem.Load() ?? new SaveData();
        Coins = saveData.coins;
        foreach (UpgradeType type in System.Enum.GetValues(typeof(UpgradeType)))
        {
            string key = type.ToString();
            if (saveData.upgradeLevels.ContainsKey(key))
                upgrades[type] = saveData.upgradeLevels[key];
            else
                upgrades[type] = type == UpgradeType.TapMultiplier ? 1 : 0; 
        }

        OnCoinsChanged?.Invoke(Coins);
        OnUpgradesLoaded?.Invoke();

        InvokeRepeating(nameof(AutoCollect), 1f, 1f);
    }
    private void OnApplicationQuit() => Save();
    private void OnApplicationPause(bool pause)
    {
        if (pause) Save();
    }
    private void Save()
    {
        saveData.coins = Coins;
        saveData.upgradeLevels.Clear();

        foreach (var kvp in upgrades)
        {
            saveData.upgradeLevels[kvp.Key.ToString()] = kvp.Value;
        }

        saveData.lastSessionTicks = System.DateTime.Now.Ticks;

        SaveSystem.Save(saveData);
    }
    public void SetUpgradeLevel(UpgradeType type, int level)
    {
        if (upgrades.ContainsKey(type))
            upgrades[type] = level;
        else
            upgrades.Add(type, level);

        OnCoinsChanged?.Invoke(Coins);
        OnUpgradesLoaded?.Invoke();
    }

    public void AddCoins(float amount)
    {
        Coins += amount;
        if (OnCoinsChanged == null)
        {
            print($"OnCoinsChanged is null, Coins: {Coins}");
        }
        OnCoinsChanged?.Invoke(Coins);
    }

    public void TapCollect()
    {
        var amount = 1 * TapMultiplier;
        AddCoins(amount);

        // Floating coin animation
        Vector2 screenPos = Input.mousePosition;
        FloatingCoinManager.Instance?.SpawnFloatingCoin(screenPos, Mathf.RoundToInt(amount));
    }
    private void AutoCollect()
    {
        float value = upgrades[UpgradeType.AutoCollector];
        AddCoins(value);
    }

    public void Upgrade(UpgradeType type)
    {
        upgrades[type]++;
    }
}

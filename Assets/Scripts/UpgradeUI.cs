using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;
    public Button upgradeButton;

    private UpgradeData upgradeData;
    private int currentLevel;

    public void Setup(UpgradeData data)
    {
        upgradeData = data;
        upgradeButton.onClick.AddListener(OnUpgradeClicked);
        Refresh();
    }
    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnUpgradesLoaded += Refresh;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnUpgradesLoaded -= Refresh;
    }

    public void Refresh()
    {
        currentLevel = GameManager.Instance.GetUpgradeLevel(upgradeData.upgradeType);
        nameText.text = upgradeData.upgradeName;
        levelText.text = $"Lv {currentLevel}";
        costText.text = $"Cost: {upgradeData.GetCost(currentLevel):0}";
    }

    private void OnUpgradeClicked()
    {
        GameManager.Instance.TryUpgrade(upgradeData.upgradeType);
        Refresh();
    }
}

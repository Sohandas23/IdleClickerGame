using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject upgradePrefab;
    public Transform upgradeParent;
    public List<UpgradeData> allUpgrades;

    private void Start()
    {
        foreach (var data in allUpgrades)
        {
            var go = Instantiate(upgradePrefab, upgradeParent);
            var ui = go.GetComponent<UpgradeUI>();
            ui.Setup(data);
            ui.gameObject.SetActive(true);
        }
    }
}
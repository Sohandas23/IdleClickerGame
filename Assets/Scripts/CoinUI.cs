using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private void Start()
    {
        GameManager.Instance.OnCoinsChanged += UpdateCoinText;
        UpdateCoinText(GameManager.Instance.Coins);
    }

    private void UpdateCoinText(float newAmount)
    {
        coinText.text = $"Coins: {newAmount:0}";
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnCoinsChanged -= UpdateCoinText;
    }
}

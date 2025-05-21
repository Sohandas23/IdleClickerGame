using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCoinManager : MonoBehaviour
{
    public static FloatingCoinManager Instance;
    public GameObject floatingCoinPrefab;
    public Transform canvasTransform;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnFloatingCoin(Vector2 screenPos, int amount)
    {
        var coin = Instantiate(floatingCoinPrefab, canvasTransform);
        RectTransform rect = coin.GetComponent<RectTransform>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasTransform as RectTransform, screenPos, null, out localPoint);

        rect.anchoredPosition = localPoint;
        coin.SetActive(true);
        coin.GetComponent<FloatingCoin>().Show(amount);
    }
}

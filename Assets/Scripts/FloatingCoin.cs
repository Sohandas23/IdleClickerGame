using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingCoin : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public float floatSpeed = 50f;
    public float fadeDuration = 1f;

    private CanvasGroup canvasGroup;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (coinText == null)
            coinText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Show(int amount)
    {
        coinText.text = $"+{amount}";
        canvasGroup.alpha = 1f;
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Vector3 startPos = rect.anchoredPosition;
        Vector3 endPos = startPos + Vector3.up * 100f;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            rect.anchoredPosition = Vector3.Lerp(startPos, endPos, elapsed / fadeDuration);
            canvasGroup.alpha = 1f - (elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
        Destroy(gameObject);
        
    }
}

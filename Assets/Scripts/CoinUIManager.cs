using UnityEngine;
using TMPro;

public class CoinUIManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI coinText;

    [Header("Settings")]
    public int totalCoins = 3;

    private int collectedCoins = 0;

    private void OnEnable()
    {
        CoinPickup.OnCoinCollected += UpdateCoinCount;
    }

    private void OnDisable()
    {
        CoinPickup.OnCoinCollected -= UpdateCoinCount;
    }

    private void Start()
    {
        UpdateText();
    }

    private void UpdateCoinCount()
    {
        collectedCoins++;
        UpdateText();
    }

    private void UpdateText()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {collectedCoins} / {totalCoins}";
        }
    }
}
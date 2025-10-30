using UnityEngine;

public class UnlockObject : MonoBehaviour
{
    [Header("Coin Settings")]
    public int totalCoins = 3; 

    private int collectedCoins = 0;
    private bool isUnlocked = false;

    private void OnEnable()
    {
        CoinPickup.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        CoinPickup.OnCoinCollected -= HandleCoinCollected;
    }

    private void HandleCoinCollected()
    {
        collectedCoins++;

        if (collectedCoins >= totalCoins && !isUnlocked)
        {
            Unlock();
        }
    }

    private void Unlock()
    {
        isUnlocked = true;
        Debug.Log("All coins collected! Object disappearing...");
        gameObject.SetActive(false);
    }
}
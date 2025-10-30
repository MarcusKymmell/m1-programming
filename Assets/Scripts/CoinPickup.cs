using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public delegate void CoinCollected();
    public static event CoinCollected OnCoinCollected;

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.GetComponent<CharacterController>())
        {
           
            OnCoinCollected?.Invoke();

           
            Destroy(gameObject);
        }
    }
}
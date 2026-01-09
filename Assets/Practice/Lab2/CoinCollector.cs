using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] int Coins = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Coins += 1;
            Destroy(other.gameObject);
        }
    }
}

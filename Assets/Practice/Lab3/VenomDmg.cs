using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class VenomDmg : MonoBehaviour
{
    [SerializeField] float Amount = 1; // Урон в N секунд
    [SerializeField] float BurstDamage = 0; // Первичный урон
    
    [SerializeField] float VenomInterval = 1f;
    [SerializeField] float VenomDuration = 3f;
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth health))
        {
            health.TakeDamage(BurstDamage);
            health.StartCoroutine( health.TakeVenomDamage(Amount, VenomDuration, VenomInterval) );
        }
    }
}

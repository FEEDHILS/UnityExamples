using UnityEngine;

public class BasicDmg : MonoBehaviour
{
    [SerializeField] float Amount = 10;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth health))
            health.TakeDamage(Amount);
    }
}

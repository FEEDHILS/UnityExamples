using Unity.Mathematics;
using UnityEngine;
using System.Collections;

public interface IHurt
{
    public void DealDamage(PlayerHealth health);
}

public class PlayerHealth : MonoBehaviour
{
    [SerializeField][Range(0, 100)] float Health = 100;
    public void TakeDamage(float dmg)
    {
        Health = math.max(Health - dmg, 0);

        if (Health <= 0)
            Die();
    }

    public IEnumerator TakeVenomDamage(float Amount, float Duration, float Interval)
    {
        float current = 0f;
        
        while (current < Duration)
        {
            yield return new WaitForSeconds(Interval);
            TakeDamage(Amount);
            current += Interval;
        }
    }

    void Die()
    {
        print("Player is dead");
        Destroy(gameObject);
    }
}

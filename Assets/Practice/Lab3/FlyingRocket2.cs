using System.Collections;
using UnityEngine;

public class FlyingRocket2 : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float detonationTime;
    [SerializeField] GameObject explosion;

    void Start() => StartCoroutine(RocketLife());
    IEnumerator RocketLife()
    {
        StartCoroutine(Move());
        yield return new WaitForSeconds(detonationTime);
        Detonate();
    }


    IEnumerator Move()
    {
        while (true)
        {
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    
    void Detonate()
    {
        Debug.Log("Detonated");
        Destroy(Instantiate(explosion, transform.position, transform.rotation), explosion.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Detonate();
    }
}

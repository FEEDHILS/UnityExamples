using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject Prefab;
    [SerializeField] Vector3 Offset;
    [SerializeField][Range(0, 1)] float Probability = 0.4f; 

    [ContextMenu("Spawn Manually")]
    void Start()
    {
        if (Random.value <= Probability)
            Instantiate(Prefab, transform.position + Offset, Prefab.transform.rotation, transform);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + Offset, .05f);
        // Gizmos.DrawWireMesh(Prefab.GetComponent<MeshFilter>().sharedMesh, 0, transform.position + Offset, Prefab.transform.rotation, Prefab.transform.lossyScale);
    }
}

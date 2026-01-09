using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class TreePlacer : MonoBehaviour
{
    [SerializeField] Mesh[] Trees;
    [SerializeField] Material TreeMaterial;
    [SerializeField] Vector3 TreeSize = new Vector3(.4f, .4f, .4f);
    [SerializeField] Terrain Landscape;
    [SerializeField] BoxCollider Space;

    [Header("Random Generator")]
    [SerializeField] int Population = 1000;

    [Header("Perlin Generation")]
    [SerializeField] float PerlinStep = 1f;
    [SerializeField][Min(0.1f)] Vector2 PopulationSize;
    [SerializeField][Range(0, 1)] float PerlinThreshold = 0.4f;
    [SerializeField] bool EnableGizmos = false;

    List<Matrix4x4>[] TreeMatrices;
    bool IsReady = false;

    void Start() => ClearMatrix();

    void Update()
    {
        for (int i = 0; i < Trees.Length && IsReady; i++)
            Graphics.RenderMeshInstanced(new RenderParams(TreeMaterial), Trees[i], 0, TreeMatrices[i]);
    }

    [ContextMenu("Random Generator")]
    void RandomGenerate()
    {
        float xRange = Space.bounds.extents.x;
        float zRange = Space.bounds.extents.z;
        
        for (int i = 0; i < Population; i++)
        {
            Vector3 treePos = new Vector3( Random.Range(Space.transform.position.x - xRange, Space.transform.position.x + xRange),
                    0, Random.Range(Space.transform.position.z - zRange, Space.transform.position.z + zRange) );

            treePos.y = Landscape.SampleHeight(treePos)-0.1f;
            
            int treeType = Random.Range(0, Trees.Length);
            TreeMatrices[treeType].Add( Matrix4x4.TRS(treePos, Quaternion.identity, TreeSize) );
        }
        
        IsReady = true;
    }

    [ContextMenu("Perlin Noise Generator")]
    void PerlinGenerate()
    {
        foreach(Vector3 point in CalculatePoints(PerlinThreshold))
        {
            int treeType = Random.Range(0, Trees.Length);
            Vector3 treePos = point;
            treePos.y = Landscape.SampleHeight(point)-0.1f;
            // Instantiate( Trees[treeType], treePos, Quaternion.identity, transform);
            TreeMatrices[treeType].Add( Matrix4x4.TRS(treePos, Quaternion.identity, TreeSize) );
        }

        IsReady = true;
    }

    [ContextMenu("Clear GPU Instance Matrix")]
    void ClearMatrix()
    {
        TreeMatrices = new List<Matrix4x4>[Trees.Length];
        for(int i = 0; i < Trees.Length; i++)
            TreeMatrices[i] = new List<Matrix4x4>();
        
        IsReady = false;
    }

    List<Vector3> CalculatePoints(float threshold)
    {
        float xPopulation = Mathf.Floor( Space.bounds.extents.x * 2 );
        float zPopulation = Mathf.Floor( Space.bounds.extents.z * 2 );

        List<Vector3> AllPoints = new List<Vector3>();

        for(float x = 0; x < xPopulation; x += PopulationSize.x)
        {
            for(float z = 0; z < zPopulation; z += PopulationSize.y)
            {
                Vector3 Pos = new Vector3(Space.bounds.min.x + x, 0, Space.bounds.min.z + z);

                Pos.y = Mathf.PerlinNoise(Pos.x / PerlinStep, Pos.z / PerlinStep);

                if (Pos.y > threshold)
                    AllPoints.Add(Pos);
            }
        }

        return AllPoints;
    }

    void OnDrawGizmosSelected()
    {
        if (!EnableGizmos)
            return;

        foreach(Vector3 point in CalculatePoints(-1)) // -1 потому что забираем все точки.
        {
            if (point.y > PerlinThreshold)
                    Gizmos.color = Color.green;
                else
                    Gizmos.color = Color.white;
            
            Gizmos.DrawWireSphere(point + Vector3.up * Landscape.SampleHeight(point), 0.1f);
        }
    }

}

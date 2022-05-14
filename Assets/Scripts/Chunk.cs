using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chunk : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public GameObject checkpointObj;
    public checkpoint checkpoint;
    public int mobCount;

    public Mesh[] BlockMeshes;
    void Start()
    {

        foreach (var filter in GetComponentsInChildren<MeshFilter>())
        {
            if (filter.sharedMesh==BlockMeshes[0])
            {
                int x = Random.Range(0, BlockMeshes.Length);
                filter.sharedMesh = BlockMeshes[x];
                filter.GetComponent<MeshCollider>().sharedMesh = BlockMeshes[x];
            }
        }
    }
    void Update()
    {
        
    }
    public void removeCheckpoint()
    {
        if(checkpointObj)
            Destroy(checkpointObj);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChunkSpawner : MonoBehaviour
{
    public Transform player;
    public checkpoint lastCheckpoint;
    public checkpoint nextCheckpoint;

    public Chunk[] ChunkPrefabs;
    public Chunk FirstChunk;
    public int totalChunkCount=1;

    public MobSpawner mobSpawner;

    public List<Chunk> spawnedChunks = new List<Chunk>();
    
    private NavMeshSurface navMeshSurface;


    void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        spawnedChunks.Add(FirstChunk);
        lastCheckpoint = FirstChunk.checkpoint;

        StartCoroutine(bakeNavMesh());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, spawnedChunks[spawnedChunks.Count-1].end.position)<100f)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        Chunk newChunk = Instantiate(ChunkPrefabs[Random.Range(0, ChunkPrefabs.Length)]);

        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].end.position - newChunk.start.localPosition;
        totalChunkCount++;
        newChunk.mobCount = totalChunkCount;

        spawnedChunks.Add(newChunk);

        if (totalChunkCount >= 4)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
        
        /*
        if ((totalChunkCount - 1) % 11 != 0)
        {
            newChunk.removeCheckpoint();
        }
        else
        {
            if (nextCheckpoint)
            {
                nextCheckpoint.check();
                lastCheckpoint = nextCheckpoint;
                nextCheckpoint = newChunk.checkpoint;
            }
            else
            {
                nextCheckpoint = newChunk.checkpoint;
            }
        }*/

        mobSpawner.lastChunk = newChunk;
        StartCoroutine(bakeNavMesh());
    }

    public void clearChunks()
    {
        for (int i = 0; i < spawnedChunks.Count; i++)
        {
            if (spawnedChunks[0].checkpoint != lastCheckpoint)
            {
                Destroy(spawnedChunks[0].gameObject);
                spawnedChunks.RemoveAt(0);
            }
            else
            {
                Destroy(spawnedChunks[0].gameObject);
                spawnedChunks.RemoveAt(0);
                break;
            }
        }
        lastCheckpoint = nextCheckpoint;
        nextCheckpoint = null;
    }


    IEnumerator bakeNavMesh()
    {
        yield return new WaitForSeconds(0.5f);
        navMeshSurface.BuildNavMesh();
    }
}

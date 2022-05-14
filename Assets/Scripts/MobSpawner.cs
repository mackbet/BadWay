using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public mobContainer mobContainer;
    public Transform player;
    public GameObject[] mobsPrefabs;
    public Chunk lastChunk;
    void Start()
    {
        StartCoroutine(spawnMob(mobsPrefabs[0], 5f));
        StartCoroutine(spawnMob(mobsPrefabs[1], 4f));
    }
    void Update()
    {
        
    }

    IEnumerator spawnMob(GameObject mob, float timer)
    {
        while (true)
        {
            if (lastChunk.mobCount > 0)
            {
                Vector3 newPos = new Vector3(lastChunk.end.position.x, lastChunk.end.position.y + mob.transform.position.y, lastChunk.end.position.z);
                GameObject newMob = Instantiate(mob, newPos, Quaternion.LookRotation(player.position - newPos, Vector3.up));
                newMob.GetComponent<mobsTarget>().target = player;
                newMob.transform.parent = mobContainer.transform;

                lastChunk.mobCount--;
            }
            yield return new WaitForSeconds(timer);
        }
    }

    public void resetPlayer()
    {
        foreach (var mobTarget in mobContainer.GetComponentsInChildren<mobsTarget>())
        {
            mobTarget.target = player;
        }
    }
}

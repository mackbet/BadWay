using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public GameObject playerPrefab;

    public Transform target;

    public ChunkSpawner chunkSpawner;
    public MobSpawner mobSpawner;

    public GameObject pause;
    void Start()
    {
        pause = target.transform.Find("Canvas").transform.Find("pause").gameObject;
    }

    void Update()
    {
        if (target.GetComponent<playerIndicators>().HP <= 0 || target.position.y < -20)
        {
            die();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void revive()
    {
        Destroy(target.gameObject);
        Vector3 checkpoint;
        if (chunkSpawner.nextCheckpoint && chunkSpawner.nextCheckpoint.isChecked)
        {
            checkpoint = chunkSpawner.nextCheckpoint.transform.position;
            chunkSpawner.clearChunks();
        }
        else
        {
            checkpoint = chunkSpawner.lastCheckpoint.transform.position;
        }
        target = Instantiate(playerPrefab, checkpoint, Quaternion.identity).transform;
        pause = target.transform.Find("Canvas").transform.Find("pause").gameObject;

        chunkSpawner.player = target;

        mobSpawner.player = target;
        mobSpawner.mobContainer.player = target.GetComponent<playerIndicators>();
        mobSpawner.resetPlayer();
    }

    public void die()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(0);
    }
}

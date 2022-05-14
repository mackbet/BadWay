using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomDestroyer : MonoBehaviour
{
    public float percent;
    void Start()
    {
        if (Random.Range(0, 100) < percent * 100)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}

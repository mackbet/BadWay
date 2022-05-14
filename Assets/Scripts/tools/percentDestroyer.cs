using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class percentDestroyer : MonoBehaviour
{
    public float percent;
    void Start()
    {
        float childCount = transform.childCount;
        float currentChildCount = transform.childCount;
        int limit = 500;
        while (limit-- > 0 && Mathf.RoundToInt(childCount * percent) != currentChildCount)
        {
            Destroy(transform.GetChild(Random.Range(0, transform.childCount)).gameObject);
            currentChildCount--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

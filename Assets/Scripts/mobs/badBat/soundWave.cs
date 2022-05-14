using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundWave : MonoBehaviour
{
    public float speed;
    public float damage=10f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject,5f);
    }
    
    void Update()
    {
        rb.velocity = transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="player")
        {
            other.GetComponent<playerIndicators>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}

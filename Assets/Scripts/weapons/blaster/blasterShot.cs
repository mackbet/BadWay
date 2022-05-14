using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blasterShot : MonoBehaviour
{
    public GameObject ShotParticle;
    public GameObject CritParticle;
    public Color ShotParticleColor;
    public Quaternion ShotParticleQuaternion;
    public Vector3 targetPosition;
    public float speed;
    public bool crit = false;

    void Start()
    {
        StartCoroutine(remove());
    }

    void Update()
    {
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        }
        else
        {
            if (crit)
            {
                Instantiate(CritParticle, targetPosition, Quaternion.identity);
            }

            ParticleSystem ps = Instantiate(ShotParticle, targetPosition, ShotParticleQuaternion).GetComponent<ParticleSystem>();
            ps.startColor = ShotParticleColor;


            Destroy(gameObject);
        }

    }
    IEnumerator remove()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}

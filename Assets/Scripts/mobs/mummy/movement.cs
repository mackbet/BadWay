using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class movement : MonoBehaviour
{
    public Transform target;
    public Animator animator;
    private NavMeshAgent agent;
    //угол обзора
    public float visionRange;
    //видимость после ухода
    public float visionTime;
    public float currentVisionTime;
    //интервал атаки
    public float attackCooldown = 2f;
    public float currentAttackCooldown = 0;
    //урон
    public float damage = 15;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(isKinematicTrue());
    }

    void Update()
    {
        if (!agent.isOnNavMesh)
        {
            return;
        }
        if (!target)
        {
            target = GetComponent<mobsTarget>().target;
            currentVisionTime = 0;
        }
        if (transform.position.y<-10f)
        {
            Destroy(gameObject);
        }

        float angle = Vector3.Angle(transform.forward, target.position - transform.position);
        float distance = Vector3.Distance(transform.position, target.position);

        if ((inView(angle, distance) && !smthBetween()) || isNear())
        {
            currentVisionTime = visionTime;
        }

        if (currentVisionTime>0)
        {
            agent.SetDestination(target.position);
            animator.SetBool("running", true);
            if (Vector3.Distance(target.position, transform.position) < 3f && currentAttackCooldown <= 0)
            {
                attack();
            }
        }
        else
        {
            agent.SetDestination(transform.position);
            animator.SetBool("running", false);
        }


        currentVisionTime -= Time.deltaTime;
        currentAttackCooldown -= Time.deltaTime;
    }

    private bool inView(float angle, float distance)
    {
        if ((angle <= 65 && distance < visionRange))
        {
            return true;
        }

        return false;
    }
    private bool smthBetween()
    {
        Vector3 lookVector = target.position - transform.position;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, lookVector);
        Physics.Raycast(ray, out hit);
        if (hit.transform == target)
        {
            return false;
        }
        return true;
    }
    private bool isNear()
    {
        if (Vector3.Distance(transform.position, target.position) < 7f)
            return true;

        return false;
    }

    private void attack()
    {
        target.GetComponent<playerIndicators>().TakeDamage(damage);
        currentAttackCooldown = attackCooldown;
    }

    IEnumerator isKinematicTrue()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody>().isKinematic = true;
    }
}

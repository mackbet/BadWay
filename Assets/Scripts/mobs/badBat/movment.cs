using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour
{
    public Transform target;
    //��������
    public float rotationSpeed;
    public float moveSpeed;
    //���� ������
    public float visionRange;
    //��������� ����� �����
    public float visionTime;
    public float currentVisionTime;
    //������ �����
    public float flightAltitude=5f;
    //������ �����������
    public float repulsionRadius=2f;
    //�������� �����
    public float attackCooldown = 2f;
    public float currentAttackCooldown = 0;
    //�����
    public GameObject attackPrefab;

    private Rigidbody rb;
    private float rotateAroundSide;
    void Start()
    {
        moveSpeed *= 0.1f;
        rb = GetComponent<Rigidbody>();

        if (Random.Range(0, 100) > 50)
            rotateAroundSide = 1;
        else
            rotateAroundSide = -1;

        target = GetComponent<mobsTarget>().target;
    }

    void Update()
    {
        if (!target)
        {
            target = GetComponent<mobsTarget>().target;
            currentVisionTime = 0;
        }

        float angle = Vector3.Angle(transform.forward, target.position - transform.position);
        float distance = Vector3.Distance(transform.position, target.position);

        //����� ��� ���
        if ((inView(angle,distance) && !smthBetween()) || isNear())
            currentVisionTime = visionTime;


        //�������� �������
        Vector3 moveDir=Vector3.zero;

        //���� ����� ������ 
        if (currentVisionTime > 0)
        {

            Vector3 deltaV = target.position - transform.position;
            deltaV.y = 0;
            if (distance < 10)//�����
            {
                moveDir -= deltaV * moveSpeed;
            }
            else if (distance > 15)//������
            {

                moveDir += deltaV * moveSpeed;
            }

            //�������� �����������
            if (smthBetween() && Vector3.Distance(transform.position,target.position)<20)
            {
                transform.RotateAround(target.position, Vector3.up, rotateAroundSide * moveSpeed);
            }
            if (!rotateMob() && inView(angle, distance) && !smthBetween() && currentAttackCooldown<=0)
            {
                attack();
            }
        }
        
        //�������� ��� �����
        RaycastHit hit;
        Ray ray =new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, 100f);
        if (hit.collider)
        {
            if(transform.position.y - hit.transform.position.y < flightAltitude)
            {
                moveDir.y = moveSpeed;
            }
            else
            {
                moveDir.y = -moveSpeed;
            }
        }
        else
        {
            if (transform.position.y - target.position.y < flightAltitude)
            {
                moveDir.y = moveSpeed;
            }
            else
            {
                moveDir.y = -moveSpeed;
            }
        }

        //������������ �� ��������
        Collider[] colliders = Physics.OverlapSphere(transform.position, repulsionRadius);
        foreach (var collider in colliders)
        {
            if (collider.transform.parent && collider.transform.parent.gameObject!=gameObject)
            {
                Vector3 delta = collider.transform.position - transform.position;
                moveDir -= delta * moveSpeed;
            }
        }

        rb.velocity = moveDir;


        currentVisionTime -= Time.deltaTime;
        currentAttackCooldown -= Time.deltaTime;
    }
    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, (target.position - transform.position));
            Gizmos.DrawRay(transform.position, transform.forward*100f);
        }
    }
    private bool rotateMob()
    {
        Vector3 lookVector = target.position - transform.position;
        if (lookVector == Vector3.zero) return false;
        Quaternion newRotation = Quaternion.RotateTowards
        (
            transform.rotation,
            Quaternion.LookRotation(lookVector,Vector3.up),
            rotationSpeed*Time.deltaTime
        );

        float deltaRotation = Vector3.Distance(newRotation.eulerAngles, transform.rotation.eulerAngles);
        transform.rotation = newRotation;
        if (deltaRotation < 0.5f) return false;
        return true;
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
        if (Vector3.Distance(transform.position, target.position)<7f)
            return true;

        return false;
    }

    private void attack()
    {
        Instantiate(attackPrefab, transform.position + transform.forward, transform.rotation);
        currentAttackCooldown = attackCooldown;
    }

}

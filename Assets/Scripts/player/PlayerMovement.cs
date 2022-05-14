using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Vector3 velocity;
    public bool isGrounded;
    public bool lastIsGrounded;

    public AudioSource footsteps;
    public AudioSource jumpSound;
    public AudioSource landSound;

    private void Start()
    {
        lastIsGrounded = isGrounded;
    }

    void Update()
    {
        if (Time.timeScale != 1) return;
        //движ
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //шаги
        Footsteps();

        //заземление
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, 100f);
        if (hit.collider)
        {
            //Debug.Log(Vector3.Distance(hit.collider.transform.position, transform.position));
            if (Vector3.Distance(hit.collider.transform.position, transform.position) < 3.4f)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        JumpSounds();
        //притяжение
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //прыжок
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpSound.Play();
        }
        controller.Move(velocity * Time.deltaTime);


        lastIsGrounded = isGrounded;
    }

    private void Footsteps()
    {
        if(isGrounded && controller.velocity.magnitude>10f && !footsteps.isPlaying)
        {
            footsteps.pitch = Random.Range(0.9f, 1.1f);
            footsteps.Play();
        }
    }
    private void JumpSounds()
    {
        if (!lastIsGrounded && isGrounded && !landSound.isPlaying)
        {
            landSound.Play();
        }
    }
}

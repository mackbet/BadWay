using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public ParticleSystem dustYellow;
    public ParticleSystem dustBlue;
    public ParticleSystem circle;

    public Color dustColorMin;
    public Color dustColorMax;
    public Color circleColor;

    public AudioSource sound;
    public bool isChecked;

    void Start()
    {
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player" && !isChecked)
        {
            check();
        }
    }
    public void check()
    {
        isChecked = true;

        circle.startColor = circleColor;

        dustYellow.Stop();
        dustBlue.Play();
    }

}

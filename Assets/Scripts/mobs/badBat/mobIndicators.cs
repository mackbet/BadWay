using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mobIndicators : MonoBehaviour
{
    public float maxHP;
    public float HP;
    public float reward=5;
    public float experience = 1;
    public int type;

    public Canvas hpBar;
    public Image hpFill;

    public GameObject[] mobModels;
    private List<Material> mobMaterials;

    public ParticleSystem damageParticle;
    public AudioSource damageSound;
    public ParticleSystem dieParticle;

    public Transform target;
    void Start()
    {
        mobMaterials = new List<Material>();
        for (int i =0; i< mobModels.Length; i++)
        {
            mobMaterials.Add(mobModels[i].GetComponent<Renderer>().material);
        }
        //target = GetComponent<mobsTarget>().target;
        HP = maxHP;
    }
    void Update()
    {
        if(hpBar)
            hpBar.transform.LookAt(target);
    }
    public float[] TakeDamage(float damage)
    {
        HP -= damage;

        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (var collider in colliders)
        {
            if (collider.transform.parent && collider.transform.parent.tag=="mob")
            {
                Transform mob = collider.transform.parent;
                if (mob.GetComponent<movment>())
                    mob.GetComponent<movment>().currentVisionTime = mob.GetComponent<movment>().visionTime;
                else if (mob.GetComponent<movement>())
                {
                    mob.GetComponent<movement>().currentVisionTime = mob.GetComponent<movement>().visionTime;
                }
            }
        }

        if (HP <= 0)
        {
            die();
            return new float[2]{ reward, experience};
        }
        else
        {
            hpBar.gameObject.SetActive(true);
            hpFill.fillAmount = HP / maxHP;

            TakeDamageSound();
            StartCoroutine(TakeDamageAnim());
        }

        return new float[2] { 0, 0 };
    }

    IEnumerator TakeDamageAnim()
    {
        for (int i = 0; i < mobMaterials.Count; i++)
        {
            mobMaterials[i].EnableKeyword("_EMISSION");
        }

        damageParticle.Play();

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < mobMaterials.Count; i++)
        {
            mobMaterials[i].DisableKeyword("_EMISSION");
        }
        
    }

    private void TakeDamageSound()
    {
        if (!damageSound.isPlaying)
        {
            damageSound.pitch = Random.Range(0.9f, 1.1f);
            damageSound.Play();
        }
    }
    private void die (){
        transform.parent.GetComponent<mobContainer>().addKill(type);
        Instantiate(dieParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

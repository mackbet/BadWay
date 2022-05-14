using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blasterScript : MonoBehaviour
{
    //анимации
    public Animator anim;
    //урон
    public float damage=10f;
    //дальность
    public float range = 100f;
    //магазин
    public float bulletCount = 10f;
    public float currentBulletCount = 10f;
    //перезарядка
    public float reloadTime = 3f;
    public bool reloading = false;
    //время между выстрелами
    public float shotInterrupt = 0.2f;
    public float timeToShoot;
    //позиция вылета пули
    public Transform shotBegin;
    //звуки
    public AudioSource shotAudio;
    public AudioSource reloadAudio;
    //пуля
    public GameObject bullet;
    //дульная вспышка
    public GameObject[] muzzleLights;
    //крит
    public float critChance = 0;
    public float critMultiplier = 0;


    void Start()
    {
        timeToShoot = Time.time;
        reloadTime *= (100-PlayerPrefs.GetFloat("reload" + "quantity"))/ 100;

        if (PlayerPrefs.GetFloat("crit" + "quantity") > 0)
        {
            critMultiplier = 1 + (PlayerPrefs.GetFloat("crit" + "quantity") / 100);
            critChance = PlayerPrefs.GetFloat("crit" + "quantity");
        }
    }

    public float[] Shoot(Transform sight)
    {
        float[] res = { 0, 0 };

        if (Time.time >= timeToShoot && !reloading)
        {
            timeToShoot = Time.time + shotInterrupt;
            RaycastHit hit;
            if (Physics.Raycast(sight.transform.position, sight.transform.forward, out hit, range))
            {
                GameObject bulletP = Instantiate(bullet, shotBegin.position, Quaternion.LookRotation(hit.point - shotBegin.position, Vector3.up));
                if (hit.transform.tag == "mob")
                {
                    float pureDamage = damage;
                    if (Random.Range(0,99)<critChance)
                    {
                        pureDamage *= critMultiplier;
                        bulletP.GetComponent<blasterShot>().crit = true;
                    }
                    res = hit.transform.GetComponent<mobIndicators>().TakeDamage(pureDamage);
                    bulletP.GetComponent<blasterShot>().targetPosition = hit.point;
                }
                else if(hit.transform.gameObject.layer==3)
                {
                    try
                    {
                        Renderer renderer = hit.collider.GetComponent<MeshRenderer>();
                        Texture2D texture2D = renderer.material.mainTexture as Texture2D;
                        Vector2 pCoord = hit.textureCoord2;
                        pCoord.x *= texture2D.width;
                        pCoord.y *= texture2D.height;

                        Vector2 tiling = renderer.material.mainTextureScale;
                        Color color = texture2D.GetPixel(Mathf.FloorToInt(pCoord.x * tiling.x), Mathf.FloorToInt(pCoord.y * tiling.y));
                        bulletP.GetComponent<blasterShot>().ShotParticleColor = color * 1.5f;
                    }
                    finally
                    {
                        bulletP.GetComponent<blasterShot>().ShotParticleQuaternion = Quaternion.LookRotation(hit.normal, Vector3.up);
                        bulletP.GetComponent<blasterShot>().targetPosition = hit.point;
                    }
                }
            }
            else
            {
                Vector3 farPoint = sight.transform.position + sight.transform.forward * range;
                GameObject bulletP = Instantiate(bullet, shotBegin.position, Quaternion.LookRotation(farPoint - shotBegin.position, Vector3.up));
                bulletP.GetComponent<blasterShot>().targetPosition = farPoint;
            }
            StartCoroutine(ActivateMuzzle());

            anim.SetTrigger("shoot");

            shotAudio.pitch = Random.Range(1, 1.4f);
            shotAudio.Play();

            currentBulletCount--;

            if (currentBulletCount <= 0)
            {
                Reload();
            }
        }
        return new float[2] { res[0], res[1] };
    }
    IEnumerator ActivateMuzzle()
    {
        int x = Random.Range(0, muzzleLights.Length);
        muzzleLights[x].SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleLights[x].SetActive(false);
    }
    public void Reload()
    {
        StartCoroutine(ReloadCur());
    }
    IEnumerator ReloadCur()
    {
        if (currentBulletCount < bulletCount && !reloading)
        {
            float animSpeed = 1 + PlayerPrefs.GetFloat("reload" + "quantity")*2 / 100;
            Debug.Log(animSpeed);
            anim.SetFloat("reloadSpeed", animSpeed);
            anim.SetTrigger("reload");

            reloading = true;
            reloadAudio.Play();
            yield return new WaitForSeconds(reloadTime);

            currentBulletCount = bulletCount;
            reloading = false;
        }
    }
}

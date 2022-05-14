using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooting : MonoBehaviour
{
    public Camera camera;

    public blasterScript weapon;
    public playerIndicators _playerIndicators;

    //canvas
    public Text bulletsCount;

    void Start()
    {

    }

    void Update()
    {
        if (Time.timeScale != 1) return;

        if (Input.GetMouseButton(0))
        {
            float[] res = weapon.Shoot(camera.transform);
            if (res[0]>0)
                _playerIndicators.addCoins(res[0]);

            if (res[1] > 0)
                _playerIndicators.addExperience(res[1]);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            weapon.Reload();
        }

        bulletsCount.text = weapon.currentBulletCount.ToString();
    }
}

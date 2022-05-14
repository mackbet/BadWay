using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobContainer : MonoBehaviour
{
    public int killed=0;
    public int killedBest = 0;
    public playerIndicators player;
    void Start()
    {
        killedBest = PlayerPrefs.GetInt("killedBest");
    }

    void Update()
    {
    }

    public void addKill(int type)
    {
        killed++;
        if (killed % 5 == 0)
        {
            player.addSoul(1);
        }
        //ÎÁÍÎÂËÅÍÈÅ ÐÅÊÎÐÄÀ
        if (killed > killedBest)
        {
            killedBest = killed;
            PlayerPrefs.SetInt("killedBest", killedBest);
        }

        if (PlayerPrefs.GetString("reload") != "opened" && killed>=50)
        {
            PlayerPrefs.SetString("reload", "opened");
        }
        if (PlayerPrefs.GetString("crit") != "opened" && killed >= 100)
        {
            PlayerPrefs.SetString("crit", "opened");
        }
        if (PlayerPrefs.GetString("lifesteal") != "opened" && killed >= 200)
        {
            PlayerPrefs.SetString("lifesteal", "opened");
        }
    }
}

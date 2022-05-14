using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerIndicators : MonoBehaviour
{
    //хп
    public float maxHP = 100;
    public float HP = 100;
    public Image hpFill;

    //деньги
    public float moneyCount = 0;
    public Text moneyCountTxt;

    //души
    public string soulKey = "soul";
    public int soulCount = 0;
    public Text soulCountTxt;

    //уровень
    public int lvl = 0;
    public float totalExperience = 0;
    public float currentExperience = 0;
    public float necessaryExperience = 50f;
    public Image expFill;
    public Text lvlTxt;


    public float bonusGold;
    public float damageReduction=1;
    public float expMultiplier = 1;

    void Start()
    {
        soulCount = PlayerPrefs.GetInt(soulKey);

        bonusGold = PlayerPrefs.GetFloat("bonusGold" + "quantity");

        maxHP += PlayerPrefs.GetFloat("addHP" + "quantity");
        HP += PlayerPrefs.GetFloat("addHP" + "quantity");

        damageReduction = (100 - PlayerPrefs.GetFloat("armor" + "quantity")) / 100;
        expMultiplier= (100 + PlayerPrefs.GetFloat("armor" + "quantity")) / 100;

        if (PlayerPrefs.GetString("regen") == "opened")
        {
            StartCoroutine(regeneration(PlayerPrefs.GetFloat("regen" + "quantity")));
        }

        addSoul(0);
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        HP -= (damage* damageReduction);
        hpFill.fillAmount = HP / maxHP;
    }
    public void addCoins(float count)
    {
        moneyCount += (count + bonusGold);
        moneyCountTxt.text = moneyCount.ToString();
    }

    public void addExperience(float count)
    {
        totalExperience += (count* expMultiplier);
        currentExperience += (count * expMultiplier);
        if (currentExperience > necessaryExperience)
        {
            UpgradeLvl();
        }
        expFill.fillAmount = currentExperience / necessaryExperience;
    }
    public void addSoul(int x)
    {
        soulCount+= x;
        soulCountTxt.text = soulCount.ToString();
        PlayerPrefs.SetInt(soulKey,soulCount);
    }


    public void UpgradeLvl()
    {
        currentExperience -= necessaryExperience;
        necessaryExperience += 50f;

        lvl++;
        lvlTxt.text = lvl + "<size=30> lvl.</size>";

        if (PlayerPrefs.GetString("addHP") != "opened" && lvl >= 5)
        {
            PlayerPrefs.SetString("addHP", "opened");
        }
        if (PlayerPrefs.GetString("armor") != "opened" && lvl >= 10)
        {
            PlayerPrefs.SetString("armor", "opened");
        }
        if (PlayerPrefs.GetString("regen") != "opened" && lvl >= 20)
        {
            PlayerPrefs.SetString("regen", "opened");
        }

    }

    IEnumerator regeneration(float heal)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (HP + heal > maxHP)
                HP = maxHP;
            else
                HP += heal;

            hpFill.fillAmount = HP / maxHP;
        }
    }
}

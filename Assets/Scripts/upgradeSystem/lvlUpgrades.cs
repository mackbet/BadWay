using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lvlUpgrades : MonoBehaviour
{
    public string soulKey = "soul";
    public int soulsCount;
    public Text soulsTxt;
    public Text alertUI;
    private Coroutine alertCoroutine;

    void Start()
    {
        PlayerPrefs.SetString("crit", "opened");
        PlayerPrefs.SetInt(soulKey, 1000);
        PlayerPrefs.SetString("bonusGold", "opened");
        updateSouls();
    }

    void Update()
    {

    }

    public void updateSouls()
    {
        soulsCount = PlayerPrefs.GetInt(soulKey);
        soulsTxt.text = soulsCount.ToString();
    }

    public void decreaseSouls(int count)
    {
        soulsCount -= count;
        PlayerPrefs.SetInt(soulKey, soulsCount);
        soulsTxt.text = soulsCount.ToString();
    }

    public void alert(string txt)
    {
        alertUI.gameObject.SetActive(true);
        alertUI.text = txt;
        if(alertCoroutine!=null)
            StopCoroutine(alertCoroutine);
        alertCoroutine=StartCoroutine(hideAlert(txt));
    }

    IEnumerator hideAlert(string txt)
    {
        yield return new WaitForSeconds(1);
        if (alertUI.text == txt)
        {
            alertUI.gameObject.SetActive(false);
        }
    }

    public void closeCard(string txt)
    {
        PlayerPrefs.SetString(txt, "");
        PlayerPrefs.SetInt(txt+ "CurrentUpgrade", 0);
        PlayerPrefs.SetFloat(txt + "quantity", 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeCard : MonoBehaviour
{
    public bool opened;
    public string bonusName;
    public float quantity;
    public float UpgradeStepQuantity;
    public int currentUpgradeLvl;
    public int maxUpgradesLvl;
    public int priceQuantity;
    public int priceStep;

    public Sprite image;
    public string title;
    public string text;
    public Color color;

    public Image imageUI;
    public Image card;
    public Text titleUI;
    public Text textUI;
    public GameObject line;
    public Image lineFill;
    public GameObject price;
    public Text priceTxt;
    public ParticleSystem particleSystem;
    public lvlUpgrades _lvlUpgrades;
    void Start()
    {
        if (PlayerPrefs.GetString(bonusName)=="opened")
        {
            open();
            //change pressed color
            ColorBlock colors = GetComponent<Button>().colors;
            colors.pressedColor = Color.yellow;
            GetComponent<Button>().colors = colors;

            opened = true;
        }
    }

    void Update()
    {
    }

    public void open()
    {
        imageUI.sprite = image;
        card.color = color;
        titleUI.text = title;

        currentUpgradeLvl = PlayerPrefs.GetInt(bonusName + "CurrentUpgrade");
        quantity = currentUpgradeLvl * UpgradeStepQuantity;
        priceQuantity = (currentUpgradeLvl + 1) * priceStep;

        textUI.text = text.Replace("quantity",quantity.ToString()).Replace("NEWLINE", "\n");

        particleSystem.startColor = color;
        particleSystem.gameObject.SetActive(true);

        price.SetActive(true);
        priceTxt.color = color;
        priceTxt.text = priceQuantity.ToString();

        line.SetActive(true);
        if (currentUpgradeLvl == maxUpgradesLvl)
        {
            lineFill.fillAmount = 1;
            lineFill.color = Color.green;
        }
        else
            lineFill.fillAmount = (1.0f * currentUpgradeLvl) / (1.0f * maxUpgradesLvl);
    }
    public void update()
    {
        currentUpgradeLvl = PlayerPrefs.GetInt(bonusName + "CurrentUpgrade");
        quantity = currentUpgradeLvl * UpgradeStepQuantity;
        PlayerPrefs.SetFloat(bonusName + "quantity", quantity);

        priceQuantity = (currentUpgradeLvl + 1) * priceStep;

        textUI.text = text.Replace("quantity", quantity.ToString()).Replace("NEWLINE", "\n");

        price.SetActive(true);
        priceTxt.color = color;
        priceTxt.text = priceQuantity.ToString();

        if (currentUpgradeLvl == maxUpgradesLvl)
        {
            lineFill.fillAmount = 1;
            lineFill.color = Color.green;
        }
        else
            lineFill.fillAmount = (1.0f * currentUpgradeLvl) / (1.0f * maxUpgradesLvl);
    }


    public void cardClicked()
    {
        if (opened)
        {
            int soulCount = _lvlUpgrades.soulsCount;
            if (soulCount >= priceQuantity && currentUpgradeLvl<maxUpgradesLvl)
            {
                currentUpgradeLvl++;
                PlayerPrefs.SetInt(bonusName + "CurrentUpgrade", currentUpgradeLvl);
                _lvlUpgrades.decreaseSouls(priceQuantity);
                update();
            }
            else if(soulCount < priceQuantity && currentUpgradeLvl < maxUpgradesLvl)
            {
                _lvlUpgrades.alert("Недостаточно душ");
            }
            else if (currentUpgradeLvl==maxUpgradesLvl)
            {
                _lvlUpgrades.alert("Максимальный уровень");
            }
        }
        else
        {
            _lvlUpgrades.alert("Условие не выполнено");
        }
    }
}

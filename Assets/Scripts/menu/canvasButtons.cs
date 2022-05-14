using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasButtons : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void back(GameObject needToHide)
    {
        needToHide.gameObject.SetActive(false);
    }
    public void continueGame(GameObject needToHide)
    {
        needToHide.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void openUpgrades(lvlUpgrades upgrades)
    {
        upgrades.gameObject.SetActive(true);
        upgrades.updateSouls();
    }
    public void quitGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void quitApplication()
    {
        Application.Quit();
    }
}

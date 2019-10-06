using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject settings;
    [SerializeField]
    private GameObject menu;

    bool isHidden = false;
    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial");
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleSettings()
    {
        isHidden = !isHidden;
        settings.SetActive(isHidden);
        menu.SetActive(!isHidden);
    }
}

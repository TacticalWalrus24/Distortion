using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuScipt : MonoBehaviour
{
    [SerializeField]
    private GameObject normal;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private Transform player;

    bool isHidden = false;
    bool canToggle = true;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canToggle){
            ToggleMenu();
            canToggle = false;
        }
        if (Input.GetKeyUp(KeyCode.Escape) && !canToggle)
        {
            canToggle = true;
        }
    }

    public void ToggleMenu()
    {
        isHidden = !isHidden;
        menu.SetActive(isHidden);
        normal.SetActive(!isHidden);
        player.GetComponent<PlayerLookScript>().lockCursor = !isHidden;
        if (!isHidden)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }
}

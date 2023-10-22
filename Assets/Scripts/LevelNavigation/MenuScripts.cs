using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{

    public GameObject menuPanel;

    public void ToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenMenu() {
        menuPanel.SetActive(true);
    }
    
    public void CloseMenu() {
        menuPanel.SetActive(false);
    }
}

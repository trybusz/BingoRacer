using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{

    private GameObject menuPanel;

    private void Start() {
        menuPanel = GameObject.Find("InLevelMenu");
        menuPanel.SetActive(false);
    }

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

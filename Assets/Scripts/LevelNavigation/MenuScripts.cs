using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{

    public GameObject menuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void openMenu() {
        menuPanel.SetActive(true);
    }
    public void closeMenu() {
        menuPanel.SetActive(false);
    }
}

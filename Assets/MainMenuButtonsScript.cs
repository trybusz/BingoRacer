using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toLevelSelect() {
        SceneManager.LoadScene("LevelSelector");
    }

    public void toSetControls() {
        SceneManager.LoadScene("RebindControls");
    }

    public void toTutorial() {
        SceneManager.LoadScene("OG_Tutorial");
    }
    public void quitGame() {
        Application.Quit();
    }
}

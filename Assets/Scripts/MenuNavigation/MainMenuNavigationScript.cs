using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toSingleplayer() {
        SceneManager.LoadScene("SingleplayerMenu");
    }

    public void toMultiplayer() {
        SceneManager.LoadScene("MultiplayerMenu");
    }

    public void toSetControls() {
        SceneManager.LoadScene("RebindControls");
    }

    public void toTutorial() {
        SceneManager.LoadScene("OGTutorial");
    }

    public void quitGame() {
        Application.Quit();
    }
}

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
        SceneContext.SetElement("GameMode", "Singleplayer");
        SceneManager.LoadScene("SingleplayerMenu");
    }

    public void toMultiplayer() {
        SceneContext.SetElement("GameMode", "Multiplayer");
        SceneManager.LoadScene("MultiplayerMenu");
    }

    public void toSetControls() {
        SceneManager.LoadScene("RebindControls");
    }

    public void toTutorial() {
        SceneContext.SetElement("GameMode", "Singleplayer");
        SceneContext.SetElement("Level", "OGTutorial");
        SceneManager.LoadScene("OGTutorial");
    }

    public void quitGame() {
        Application.Quit();
    }
}

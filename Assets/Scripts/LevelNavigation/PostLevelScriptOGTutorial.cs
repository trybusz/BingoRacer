using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostLevelScriptOGTutorial : MonoBehaviour {

    public void backToMainMenu() {
        SceneContext.ClearElement("Level");
        SceneManager.LoadScene("MainMenu");
    }

    public void restartLevel() {
        string levelSceneName = SceneContext.GetElement("Level");
        SceneManager.LoadScene(levelSceneName);
    }
}

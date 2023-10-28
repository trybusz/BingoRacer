using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectNavigationScript : MonoBehaviour {
    
    public void ToSingleplayerMenu() {
        SceneManager.LoadScene("SingleplayerMenu");
    }

    public void ToLevel(string levelSceneName) {
        SceneContext.SetElement("Level", levelSceneName);
        SceneManager.LoadScene(levelSceneName);
    }
}

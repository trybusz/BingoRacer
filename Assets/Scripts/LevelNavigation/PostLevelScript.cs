using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostLevelScript : MonoBehaviour {

    public void backToLevelSelect() {
        SceneManager.LoadScene(LevelAssets.GetLevelFolderSceneName(SceneManager.GetActiveScene().name));
    }

    public void restartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextLevel() {
        string currentLevelName = SceneManager.GetActiveScene().name;
        string nextLevelName = LevelAssets.GetNextLevelSceneName(currentLevelName);
        if (nextLevelName != null) {
            SceneManager.LoadScene(nextLevelName);
        }
        else {
            SceneManager.LoadScene(LevelAssets.GetLevelFolderSceneName(currentLevelName));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostLevelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

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

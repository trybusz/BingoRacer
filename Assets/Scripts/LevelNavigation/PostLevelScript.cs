using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostLevelScript : MonoBehaviour {

    public void BackToLevelSelect() {
        string levelSceneName = SceneContext.GetElement("Level");
        SceneContext.ClearElement("Level");
        SceneManager.LoadScene(LevelAssets.GetLevelFolderSceneName(levelSceneName));
    }

    public void RestartLevel() {
        string levelSceneName = SceneContext.GetElement("Level");
        SceneManager.LoadScene(levelSceneName);
    }

    public void NextLevel() {
        string levelSceneName = SceneContext.GetElement("Level");
        string nextLevelName = LevelAssets.GetNextLevelSceneName(levelSceneName);
        if (nextLevelName != null) {
            SceneContext.SetElement("Level", nextLevelName);
            SceneManager.LoadScene(nextLevelName);
        }
        else {
            SceneManager.LoadScene(LevelAssets.GetLevelFolderSceneName(levelSceneName));
        }
    }

    public void BackToBingoBoard() {
        string currentLevelSceneName = SceneContext.GetElement("Level");
        SceneContext.ClearElement("Level");
        SceneManager.UnloadSceneAsync(currentLevelSceneName);
    }
}

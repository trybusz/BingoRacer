using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{

    private GameObject menuPanel;
    private string gameMode;

    private void Start() {
        menuPanel = GameObject.Find("InLevelMenu");
        menuPanel.SetActive(false);
        gameMode = SceneContext.GetElement("GameMode");
    }

    public void OpenMenu() {
        menuPanel.SetActive(true);
        if (gameMode == null || gameMode.Equals("Singleplayer")) {
            GameObject.Find("MPMenuOptions").SetActive(false);
        } else {
            GameObject.Find("SPMenuOptions").SetActive(false);
        }
    }

    public void CloseMenu() {
        menuPanel.SetActive(false);
    }

    public void BackToLevelSelect() {
        string levelSceneName = SceneContext.GetElement("Level");
        string levelFolderSceneName = LevelAssets.GetLevelFolderSceneName(levelSceneName);
        SceneManager.LoadScene(levelFolderSceneName);
    }

    public void BackToBingoBoard() {
        for (int i = 0; i < SceneManager.sceneCount; i++) {
            if (!SceneManager.GetSceneAt(i).name.Equals("BingoBoardMenu")) {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            }
        }
    }
}

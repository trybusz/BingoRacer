using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleplayerNavigationScript : MonoBehaviour
{
    public void backToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void toOGLevelSelect() {
        SceneManager.LoadScene("OGLevelSelect");
    }
}

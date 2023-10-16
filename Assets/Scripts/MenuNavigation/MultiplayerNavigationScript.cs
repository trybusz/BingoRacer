using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerNavigationScript : MonoBehaviour
{
    public void backToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void toHostGameMenu() {
        SceneManager.LoadScene("HostGameMenu");
    }

    public void toJoinGameMenu() {
        SceneManager.LoadScene("JoinGameMenu");
    }
}

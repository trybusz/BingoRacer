using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostGameNavigationScript : MonoBehaviour
{
    public void backToMultiplayerMenu() {
        SceneManager.LoadScene("MultiplayerMenu");
    }
}

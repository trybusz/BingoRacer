using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinGameNavigationScript : MonoBehaviour
{
    public void backToMultiplayerMenu() {
        SceneManager.LoadScene("MultiplayerMenu");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class BingoBoardNavigationScript : MonoBehaviour
{
    public void QuitToMultiplayerMenu() {
        // TODO: handle other quitting things before leaving game
        // Basically disconnect from relay I think

        SceneManager.LoadScene("MultiplayerMenu");
    }
}

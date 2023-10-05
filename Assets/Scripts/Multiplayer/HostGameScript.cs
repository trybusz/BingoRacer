using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostGameScript : MonoBehaviour {

    [Serialize]
    public GameObject joinCodeText;

    public async void HostGame() {
        GameObject manager = GameObject.FindGameObjectWithTag("MultiplayerManager");
        await manager.GetComponent<MultiplayerManagerScript>().CreateRelay();
        Debug.Log("Created Relay");
        string joinCode = manager.GetComponent<MultiplayerManagerScript>().joinCode;
        joinCodeText.GetComponent<TextMeshProUGUI>().SetText("Join Code: " + joinCode);
    }

    public void StartGame() {
        NetworkManager multiplayerManager = GameObject.FindGameObjectWithTag("MultiplayerManager").GetComponent<NetworkManager>();
        NetworkManager networkManager = multiplayerManager.GetComponent<NetworkManager>();
        networkManager.SceneManager.LoadScene("BingoBoardMenu", LoadSceneMode.Single);
    }
}

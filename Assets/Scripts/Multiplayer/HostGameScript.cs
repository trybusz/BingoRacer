using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
}

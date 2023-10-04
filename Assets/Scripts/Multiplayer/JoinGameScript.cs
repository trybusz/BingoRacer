using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class JoinGameScript : MonoBehaviour
{

    [Serialize]
    public GameObject joinCodeText;

    public void JoinGame() {
        string joinCode = joinCodeText.GetComponent<TMP_InputField>().text;
        GameObject manager = GameObject.FindGameObjectWithTag("MultiplayerManager");
        manager.GetComponent<MultiplayerManagerScript>().JoinRelay(joinCode);
        Debug.Log("Joined Relay");
    }
}

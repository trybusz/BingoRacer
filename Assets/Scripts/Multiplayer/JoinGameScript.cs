using TMPro;
using Unity.Services.Lobbies;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Services.Lobbies.Models;

public class JoinGameScript : MonoBehaviour
{

    // [Serialize]
    // public GameObject joinCodeText;

    // public async void ListLobbies() {
    //     try {
    //         QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
    //     } catch (LobbyServiceException e) {
    //         Debug.Log(e);
    //     }
    // }

    // public async void JoinLobby() {
    //     Lobbies.Instance.JoinLobbyByIdAsync()
    // }

    // public void JoinGame() {
    //     string joinCode = joinCodeText.GetComponent<TMP_InputField>().text;
    //     GameObject manager = GameObject.FindGameObjectWithTag("MultiplayerManager");
    //     manager.GetComponent<MultiplayerManagerScript>().JoinRelay(joinCode);
    //     Debug.Log("Joined Relay");
    //     LobbyService.Instance.QuickJoinLobbyAsync();
    // }
}

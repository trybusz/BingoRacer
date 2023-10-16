using System;
using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostGameScript : MonoBehaviour {

    // [Serialize]
    // public GameObject joinCodeText;
    // private Lobby hostLobby;
    // private float heartbeatTimer;
    // private const float hostLobbyHeartbeatPeriod = 20f;

    // public async void CreateLobby() {
    //     try {
    //         string lobbyName = "MyLobby";
    //         int maxPlayers = 2;
    //         Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
    //         heartbeatTimer = hostLobbyHeartbeatPeriod;
    //     } catch (Exception e){
    //         Debug.Log(e);
    //     }
    // }

    // private async void Update() {
    //     if (hostLobby != null) {
    //         heartbeatTimer -= Time.deltaTime;
    //         if (heartbeatTimer < 0f) {
    //             heartbeatTimer = hostLobbyHeartbeatPeriod;

    //             await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
    //         }
    //     }
    // }

    // public async void HostGame() {
    //     GameObject manager = GameObject.FindGameObjectWithTag("MultiplayerManager");
    //     await manager.GetComponent<MultiplayerManagerScript>().CreateRelay();
    //     Debug.Log("Created Relay");
    //     string joinCode = manager.GetComponent<MultiplayerManagerScript>().joinCode;
    //     joinCodeText.GetComponent<TextMeshProUGUI>().SetText("Join Code: " + joinCode);
    //     // CreateLobbyOptions lobbyOptions = new(){IsPrivate = false};
    //     // Lobby lobby = await LobbyService.Instance.CreateLobbyAsync("MyLobby", 2, lobbyOptions);
    // }

    // public void StartGame() {
    //     GameObject multiplayerManager = GameObject.FindGameObjectWithTag("MultiplayerManager");
    //     NetworkManager networkManager = multiplayerManager.GetComponent<NetworkManager>();
    //     networkManager.SceneManager.LoadScene("BingoBoardMenu", LoadSceneMode.Single);
    //     SceneManager.LoadScene("OGLevel1");
    //     //networkManager.SceneManager.LoadScene("OGLevel1", LoadSceneMode.Additive);
    // }
}

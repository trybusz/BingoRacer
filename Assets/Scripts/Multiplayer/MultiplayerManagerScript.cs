using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class MultiplayerManagerScript : MonoBehaviour {
    public static MultiplayerManagerScript instance { get; private set; }

    // constants
    private const float hostLobbyHeartbeatPeriod = 20f;
    private const float listLobbiesCooldown = 1.1f;
    private const float updateLobbyPeriod = 1.1f;
    private const float updatePlayerCooldown = 1.1f;

    // UI objects
    public GameObject playerNameInput;
    public GameObject lobbyListHolder;
    public GameObject playerListHolder;
    public GameObject joinCodeInput;
    public GameObject lobbyCanvas;
    public GameObject readyButton;
    public GameObject teamButton;

    // utilities
    public GameObject lobbyEntryPrefab;
    public GameObject playerEntryPrefab;

    // state
    private Player player = null;
    private Lobby lobby = null;
    private bool isHost = false;
    private bool inLobby = false;
    private string joinCode = "NotSet";
    private float hostLobbyHeartbeatTimer = 0f;
    private float listLobbiesTimer = 0f;
    private float updateLobbyTimer = 0f;
    private float updatePlayerTimer = 0f;
    private bool playerUpdateNeeded = false;

    void Awake() {
        // Prevent multiple instances of the MultiplayerManager
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    // authenticate / sign in anonymously
    async void Start() {
        lobbyCanvas.SetActive(false);
        try {
            await UnityServices.InitializeAsync();

            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            player = new Player(AuthenticationService.Instance.PlayerId, null, new(3));
            player.Data["Name"] = new(PlayerDataObject.VisibilityOptions.Member, "NoName");
            player.Data["Team"] = new(PlayerDataObject.VisibilityOptions.Member, "Red");
            player.Data["Ready"] = new(PlayerDataObject.VisibilityOptions.Member, "false");
            //player.Data.Add("ready", new(PlayerDataObject.VisibilityOptions.Member, "false"));

            DontDestroyOnLoad(gameObject);
        } catch (Exception e) {
            Debug.Log(e);
        }
    }

    private void Update() {
        HostLobbyHeartBeat();
        GetLobbyUpdate();
        UpdatePlayer();
    }

    public void CleanUp() {
        if (inLobby) {
            try {
                LobbyService.Instance.RemovePlayerAsync(lobby.Id, player.Id);
            } catch (LobbyServiceException e) {
                Debug.Log(e);
            }
        }

        try {
            AuthenticationService.Instance.SignOut();
        } catch (AuthenticationException e) {
            Debug.Log(e);
        }

        Destroy(gameObject);
    }

    public async void CreateLobby() {
        if (inLobby) return;

        SetPlayerName();

        try {
            string lobbyName = "MyLobby";
            int maxPlayers = 2;
            CreateLobbyOptions options = new() {
                IsPrivate = false,
                Player = player
            };
            lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);

            hostLobbyHeartbeatTimer = Time.time + hostLobbyHeartbeatPeriod;
            isHost = true;
            inLobby = true;
            joinCodeInput.GetComponent<TMP_InputField>().text = lobby.LobbyCode;
            
            lobbyCanvas.SetActive(true);
        } catch (LobbyServiceException e){
            Debug.Log(e);
        }
    }

    private async void HostLobbyHeartBeat() {
        if (!inLobby || !isHost || Time.time < hostLobbyHeartbeatTimer) return;
        hostLobbyHeartbeatTimer = Time.time + hostLobbyHeartbeatPeriod;

        try {
            await LobbyService.Instance.SendHeartbeatPingAsync(lobby.Id);
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }

    private async void GetLobbyUpdate() {
        if (!inLobby || Time.time < updateLobbyTimer) return;
        updateLobbyTimer = Time.time + updateLobbyPeriod;

        try {
            lobby = await LobbyService.Instance.GetLobbyAsync(lobby.Id);

            for (int i = 0; i < playerListHolder.transform.childCount; i++) {
                Destroy(playerListHolder.transform.GetChild(i).gameObject);
            }

            Instantiate(playerEntryPrefab, playerListHolder.transform);

            foreach (Player playerEntry in lobby.Players) {
                GameObject currEntry = Instantiate(playerEntryPrefab, playerListHolder.transform);
                Transform playerName = currEntry.transform.GetChild(0);
                Transform team = currEntry.transform.GetChild(1);
                Transform ready = currEntry.transform.GetChild(2);
                playerName.GetComponent<TextMeshProUGUI>().SetText(playerEntry.Data["Name"].Value);
                team.GetComponent<TextMeshProUGUI>().SetText(playerEntry.Data["Team"].Value);
                if (playerEntry.Data["Ready"].Value == "true") {
                    ready.GetComponent<TextMeshProUGUI>().SetText("Yep!");
                } else {
                    ready.GetComponent<TextMeshProUGUI>().SetText("Nope");
                }
            }

        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }

    public async void ListLobbies() {
        if (inLobby) return;
        if (Time.time < listLobbiesTimer) return;
        listLobbiesTimer = Time.time + listLobbiesCooldown;

        try {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
            List<Lobby> lobbyList = queryResponse.Results;

            for (int i = 0; i < lobbyListHolder.transform.childCount && i < 10; i++) {
                Destroy(lobbyListHolder.transform.GetChild(i).gameObject);
            }

            Instantiate(lobbyEntryPrefab, lobbyListHolder.transform);

            foreach (Lobby lobbyEntry in lobbyList) {
                GameObject currEntry = Instantiate(lobbyEntryPrefab, lobbyListHolder.transform);
                Transform lobbyName = currEntry.transform.GetChild(0);
                Transform playerCount = currEntry.transform.GetChild(1);
                lobbyName.GetComponent<TextMeshProUGUI>().SetText(lobbyEntry.Name);
                playerCount.GetComponent<TextMeshProUGUI>().SetText(lobbyEntry.Players.Count.ToString() + "/" + lobbyEntry.MaxPlayers.ToString());
            }
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }

    public async void JoinPrivateLobby() {
        joinCode = joinCodeInput.GetComponent<TMP_InputField>().text;
        SetPlayerName();

        try {
            JoinLobbyByCodeOptions options = new() {
                Player = player
            };
            await Lobbies.Instance.JoinLobbyByCodeAsync(joinCode, options);
            inLobby = true;
            isHost = false;
        } catch (Exception e) {
            Debug.Log(e);
        }
    }

    public async void JoinLobby(Lobby joinLobby) {
        SetPlayerName();

        try {
            JoinLobbyByIdOptions options = new() {
                Player = player
            };
            await Lobbies.Instance.JoinLobbyByIdAsync(joinLobby.Id, options);
            lobby = joinLobby;
            inLobby = true;
            isHost = false;
        } catch (Exception e) {
            Debug.Log(e);
        }
    }

    public void ToggleReady() {
        if (!inLobby) return;

        if (player.Data["Ready"].Value == "true") {
            readyButton.GetComponent<Image>().color = new Color(255f/255f, 239f/255f, 180f/255f);
            player.Data["Ready"].Value = "false";
        } else {
            readyButton.GetComponent<Image>().color = new Color(132f/255f, 222f/255f, 2f/255f);
            player.Data["Ready"].Value = "true";
        }

        playerUpdateNeeded = true;
    }

    public void ToggleTeam() {
        if (!inLobby) return;

        if (player.Data["Team"].Value == "Red") {
            teamButton.GetComponent<Image>().color = new Color(98f/255f, 161f/255f, 221f/255f);
            player.Data["Team"].Value = "Blue";
        } else {
            teamButton.GetComponent<Image>().color = new Color(222f/255f, 97f/255f, 100f/255f);
            player.Data["Team"].Value = "Red";
        }

        playerUpdateNeeded = true;
    }

    public void StartGame() {
        if (!inLobby || !isHost) return;

        foreach (Player playerEntry in lobby.Players) {
            if (playerEntry.Data["Ready"].Value != "true") {
                return;
            }
        }

        Debug.Log("Starting Game!");
    }

    private void SetPlayerName() {
        name = playerNameInput.GetComponent<TMP_InputField>().text;
        if (name == "") {
            name = "Anonymous";
        }
        player.Data["Name"].Value = name;
    }

    private void UpdatePlayer() {
        if (!inLobby || !playerUpdateNeeded || Time.time < updatePlayerTimer) return;
        updatePlayerTimer = Time.time + updatePlayerCooldown;

        UpdatePlayerOptions options = new() {
            Data = player.Data
        };

        try {
            LobbyService.Instance.UpdatePlayerAsync(lobby.Id, player.Id, options);
            playerUpdateNeeded = false;
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }
}

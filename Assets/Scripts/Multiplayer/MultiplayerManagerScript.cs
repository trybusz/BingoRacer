using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiplayerManagerScript : MonoBehaviour {
    public static MultiplayerManagerScript instance { get; private set; }

    // constants
    private const float HOST_LOBBY_HEARTBEAT_PERIOD = 20f;
    private const float LIST_LOBBIES_COOLDOWN = 1.1f;
    private const float UPDATE_LOBBY_PERIOD = 1.1f;
    private const float UPDATE_PLAYER_COOLDOWN = 1.1f;
    private const string PLAYER_NAME_KEY = "Name";
    private const string PLAYER_TEAM_KEY = "Team";
    private const string PLAYER_READY_KEY = "Ready";
    private const string LOBBY_JOIN_CODE_KEY = "JoinCode";
    private const string LOBBY_NO_JOIN_CODE = "";
    public const string TEAM_BLUE = "Blue";
    public const string TEAM_RED = "Red";
    private Color BLUE = new(98f/255f, 161f/255f, 221f/255f);
    private Color RED = new(222f/255f, 97f/255f, 100f/255f);

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
    public GameObject bingoBoardPrefab;

    // state
    private Player player = null;
    private Lobby lobby = null;
    private bool isHost = false;
    private bool inLobby = false;
    private bool inGame = false;
    private bool shouldLoadGame = false;
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
            DontDestroyOnLoad(gameObject);

            await UnityServices.InitializeAsync();

            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            player = new Player(AuthenticationService.Instance.PlayerId, null, new(3));
            player.Data[PLAYER_NAME_KEY] = new(PlayerDataObject.VisibilityOptions.Member, "NoName");
            player.Data[PLAYER_TEAM_KEY] = new(PlayerDataObject.VisibilityOptions.Member, TEAM_RED);
            player.Data[PLAYER_READY_KEY] = new(PlayerDataObject.VisibilityOptions.Member, "false");

            GetLobbyUpdate();
        } catch (Exception e) {
            Debug.Log(e);
        }
    }

    private void Update() {
        if (shouldLoadGame) {
            shouldLoadGame = false;
            if (isHost) {
                NetworkManager.Singleton.StartHost();
                NetworkManager.Singleton.SceneManager.LoadScene("BingoBoardMenu", LoadSceneMode.Single);
            } else {
                NetworkManager.Singleton.StartClient();
            }
        }
        if (!inGame) {
            HostLobbyHeartBeat();
            UpdatePlayer();
            GetLobbyUpdate();
        }
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
        instance = null;
    }

    public async void CreateLobby() {
        if (inLobby) return;

        SetPlayerName();

        try {
            string lobbyName = "MyLobby";
            int maxPlayers = 2;
            CreateLobbyOptions options = new() {
                IsPrivate = false,
                Player = player,
                Data = new Dictionary<string, DataObject> {
                    {LOBBY_JOIN_CODE_KEY, new DataObject(DataObject.VisibilityOptions.Member, LOBBY_NO_JOIN_CODE)}
                }
            };
            lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);

            hostLobbyHeartbeatTimer = Time.time + HOST_LOBBY_HEARTBEAT_PERIOD;
            isHost = true;
            inLobby = true;

            lobbyCanvas.SetActive(true);
        } catch (LobbyServiceException e){
            Debug.Log(e);
        }
    }

    private async void HostLobbyHeartBeat() {
        if (!inLobby || !isHost || Time.time < hostLobbyHeartbeatTimer) return;
        hostLobbyHeartbeatTimer = Time.time + HOST_LOBBY_HEARTBEAT_PERIOD;

        try {
            await LobbyService.Instance.SendHeartbeatPingAsync(lobby.Id);
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }

    private async void GetLobbyUpdate() {
        if (!inLobby || Time.time < updateLobbyTimer) return;
        updateLobbyTimer = Time.time + UPDATE_LOBBY_PERIOD;

        try {
            lobby = await LobbyService.Instance.GetLobbyAsync(lobby.Id);

            if (!isHost && lobby.Data[LOBBY_JOIN_CODE_KEY].Value != LOBBY_NO_JOIN_CODE) {
                JoinGame();
                return;
            }

            for (int i = 0; i < playerListHolder.transform.childCount; i++) {
                Destroy(playerListHolder.transform.GetChild(i).gameObject);
            }

            Instantiate(playerEntryPrefab, playerListHolder.transform);

            foreach (Player playerEntry in lobby.Players) {
                GameObject currEntry = Instantiate(playerEntryPrefab, playerListHolder.transform);
                Transform playerName = currEntry.transform.GetChild(0);
                Transform team = currEntry.transform.GetChild(1);
                Transform ready = currEntry.transform.GetChild(2);
                playerName.GetComponent<TextMeshProUGUI>().SetText(playerEntry.Data[PLAYER_NAME_KEY].Value);
                team.GetComponent<TextMeshProUGUI>().SetText(playerEntry.Data[PLAYER_TEAM_KEY].Value);
                if (playerEntry.Data[PLAYER_READY_KEY].Value == "true") {
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
        listLobbiesTimer = Time.time + LIST_LOBBIES_COOLDOWN;

        try {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
            List<Lobby> lobbyList = queryResponse.Results;

            for (int i = 0; i < lobbyListHolder.transform.childCount && i < 10; i++) {
                Destroy(lobbyListHolder.transform.GetChild(i).gameObject);
            }

            Instantiate(lobbyEntryPrefab, lobbyListHolder.transform);

            foreach (Lobby lobbyEntry in lobbyList) {
                GameObject currEntry = Instantiate(lobbyEntryPrefab, lobbyListHolder.transform);
                currEntry.GetComponent<Button>().onClick.AddListener(() => JoinLobby(lobbyEntry));
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
        string joinCode = joinCodeInput.GetComponent<TMP_InputField>().text;
        SetPlayerName();

        try {
            JoinLobbyByCodeOptions options = new() {
                Player = player
            };
            lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(joinCode, options);
            inLobby = true;
            isHost = false;

            lobbyCanvas.SetActive(true);
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
            lobby = await Lobbies.Instance.JoinLobbyByIdAsync(joinLobby.Id, options);
            inLobby = true;
            isHost = false;

            lobbyCanvas.SetActive(true);
        } catch (Exception e) {
            Debug.Log(e);
        }
    }

    public void ToggleReady() {
        if (!inLobby) return;

        if (player.Data[PLAYER_READY_KEY].Value == "true") {
            readyButton.GetComponent<Image>().color = new Color(255f/255f, 239f/255f, 180f/255f);
            player.Data[PLAYER_READY_KEY].Value = "false";
        } else {
            readyButton.GetComponent<Image>().color = new Color(132f/255f, 222f/255f, 2f/255f);
            player.Data[PLAYER_READY_KEY].Value = "true";
        }

        playerUpdateNeeded = true;
    }

    public void ToggleTeam() {
        if (!inLobby) return;

        if (player.Data[PLAYER_TEAM_KEY].Value == TEAM_RED) {
            teamButton.GetComponent<Image>().color = BLUE;
            player.Data[PLAYER_TEAM_KEY].Value = TEAM_BLUE;
        } else {
            teamButton.GetComponent<Image>().color = RED;
            player.Data[PLAYER_TEAM_KEY].Value = TEAM_RED;
        }

        playerUpdateNeeded = true;
    }

    public async void StartGame() {
        if (!inLobby || !isHost) return;

        foreach (Player playerEntry in lobby.Players) {
            if (playerEntry.Data[PLAYER_READY_KEY].Value != "true") {
                return;
            }
        }

        try {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(lobby.MaxPlayers - 1);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            RelayServerData relayServerData = new(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            UpdateLobbyOptions options = new() {
                IsPrivate = lobby.IsPrivate,
                Data = lobby.Data
            };
            options.Data[LOBBY_JOIN_CODE_KEY] = new DataObject(DataObject.VisibilityOptions.Member, joinCode);
            await LobbyService.Instance.UpdateLobbyAsync(lobby.Id, options);

            inGame = true;
            shouldLoadGame = true;
        } catch (RelayServiceException e) {
            Debug.Log(e);
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }

    private async void JoinGame() {
        if (!inLobby || isHost) return;
        try {
            string joinCode = lobby.Data[LOBBY_JOIN_CODE_KEY].Value;
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            RelayServerData relayServerData = new(joinAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            inGame = true;
            shouldLoadGame = true;
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }

    private void SetPlayerName() {
        name = playerNameInput.GetComponent<TMP_InputField>().text;
        if (name == "") {
            name = "Anonymous";
        }
        player.Data[PLAYER_NAME_KEY].Value = name;
    }

    private void UpdatePlayer() {
        if (!inLobby || !playerUpdateNeeded || Time.time < updatePlayerTimer) return;
        updatePlayerTimer = Time.time + UPDATE_PLAYER_COOLDOWN;

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

    public string GetTeam() {
        return player.Data[PLAYER_TEAM_KEY].Value;
    }

    public Lobby GetLobby() {
        return lobby;
    }

    public string GetPlayerId() {
        return player.Id;
    }

    public bool IsHost() {
        return isHost;
    }
}
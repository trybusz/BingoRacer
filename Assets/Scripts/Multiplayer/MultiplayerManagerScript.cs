using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using System.Threading.Tasks;

public class MultiplayerManagerScript : MonoBehaviour {
    public static MultiplayerManagerScript instance { get; private set; }

    public string joinCode = "NotSet";

    void Awake() {
        // Prevent multiple instances of the MultiplayerManager
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    async void Start() {
        await UnityServices.InitializeAsync();

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log("Signed In");

        DontDestroyOnLoad(gameObject);
    }

    public async Task CreateRelay() {
        try {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            RelayServerData relayServerData = new(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }

    public async void JoinRelay(string joinCode) {
        try {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new(joinAllocation, "dtls"); 

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }

    public void CleanUp() {
        AuthenticationService.Instance.SignOut();
        Debug.Log("Signed Out");
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BingoBoardManager : MonoBehaviour {
    private BingoBoard board;
    private NetworkManager networkManager;

    void Start() {
        board = new BingoBoard();
        networkManager = GameObject.FindGameObjectWithTag("MultiplayerManager").GetComponent<NetworkManager>();
    }

    void ShowBingoLevelData(int index) {
        float bestTime = board.GetBestTime(index);
        string bestTimeStr = "Unclaimed";
        if (bestTime != 0f) {
            bestTimeStr = LevelAssets.ConvertTimeToString(bestTime);
        }

        // display bestTimeStr
    }

    void PlayBingoLevel(int index) {
        string levelSceneName = board.GetLevelName(index);
        networkManager.SceneManager.LoadScene(levelSceneName, LoadSceneMode.Additive);
    }

    // add functions for updating the bingo board
    // when player beats a level
}

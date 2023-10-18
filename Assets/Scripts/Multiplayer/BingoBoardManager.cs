using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BingoBoardManager : MonoBehaviour {
    private BingoBoard board;
    private NetworkManager networkManager;

    void Start() {
        board = new BingoBoard();
        networkManager = GameObject.FindGameObjectWithTag("MultiplayerManager").GetComponent<NetworkManager>();
    }

    public void ShowBingoLevelData(int index) {
        float bestTime = board.GetBestTime(index);
        string bestTimeStr = "Unclaimed";
        if (bestTime != 0f) {
            bestTimeStr = LevelAssets.ConvertTimeToString(bestTime);
        }

        // display bestTimeStr
        Debug.Log(bestTimeStr);
    }

    public void PlayBingoLevel(int index) {
        string levelSceneName = board.GetLevelName(index);
        networkManager.SceneManager.LoadScene(levelSceneName, LoadSceneMode.Single);
    }

    // add functions for updating the bingo board
    // when player beats a level
}

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
    private MultiplayerManagerScript multiplayerScript;

    private Color BLUE = new(98f/255f, 161f/255f, 221f/255f);
    private Color RED = new(222f/255f, 97f/255f, 100f/255f);

    void Start() {
        board = new BingoBoard();
        networkManager = GameObject.FindGameObjectWithTag("MultiplayerManager").GetComponent<NetworkManager>();
        multiplayerScript = GameObject.FindGameObjectWithTag("MultiplayerManager").GetComponent<MultiplayerManagerScript>();
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
        // // temporary code for testing purposes
        // int team = multiplayerScript.GetTeam() == MultiplayerManagerScript.TEAM_RED ? BingoBoard.TEAM1 : BingoBoard.TEAM2;
        // int winner = board.SubmitTime(index, team, 1f);
        // if (winner != BingoBoard.NONE) {
        //     for (int i = 0; i < 25; i++) {
        //         board.SubmitTime(i, winner, 0f);
        //         UpdateSquareColor(i);
        //     }
        //     // declare winner!
        // }
        // UpdateSquareColor(index);
        string levelSceneName = board.GetLevelName(index);
        SceneContext.SetElement("Level", levelSceneName);
        SceneContext.SetElement("BingoIndex", index.ToString());
        SceneManager.LoadScene(levelSceneName, LoadSceneMode.Additive);
    }

    private void UpdateSquareColor(int index) {
        int team = board.GetTeam(index);
        GameObject square = gameObject.transform.parent.GetChild(index + 2).gameObject;
        if (team == BingoBoard.TEAM1) {
            square.GetComponent<Image>().color = RED;
        } else if (team == BingoBoard.TEAM2) {
            square.GetComponent<Image>().color = BLUE;
        }
    }

    public void SubmitLevelTime(int index, float time) {
        int team = multiplayerScript.GetTeam() == MultiplayerManagerScript.TEAM_RED ? BingoBoard.TEAM1 : BingoBoard.TEAM2;
        board.SubmitTime(index, team, time);
        UpdateSquareColor(index);
    }
    // add functions for updating the bingo board
    // when player beats a level
}

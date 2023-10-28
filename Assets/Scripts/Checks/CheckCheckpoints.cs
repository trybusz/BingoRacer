using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using System;

public class CheckCheckpoints : MonoBehaviour
{
    public GameObject[] checkpoints;
    private TMP_Text finalTimeText = null;
    private GameObject endPanel;
    private TimeScript timeScript;
    private ShowCheckpointTimeScript showCheckpointTimeScript;
    private GameObject player;
    private string gameMode;

    void Start() {
        finalTimeText = GameObject.Find("FinalTimeUI").GetComponent<TMP_Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        timeScript = player.GetComponent<TimeScript>();
        showCheckpointTimeScript = player.GetComponent<ShowCheckpointTimeScript>();
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

        endPanel = GameObject.Find("FinishedLevelCanvas");
        endPanel.SetActive(false);
        gameMode = SceneContext.GetElement("GameMode");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!AllCollected()) {
            return;
        }

        string levelSceneName = SceneContext.GetElement("Level");
        string levelfolderSceneName = LevelAssets.GetLevelFolderSceneName(levelSceneName);
        LevelTimesData levelTimes = new();
        float bestTime = levelTimes.GetLevelTime(levelfolderSceneName, levelSceneName);

        float finalTime = timeScript.GetRunTime();
        string finalTimeString = LevelAssets.ConvertTimeToString(finalTime);

        StopPlayer();
        ActivateEndScreen();
        UpdateBingoBoard(finalTime);

        finalTimeText.SetText("Time: " + finalTimeString);
        gameObject.GetComponent<DisplayMedalsScript>().displayMedals(finalTime);

        if (finalTime < bestTime) {
            levelTimes.UpdateLevelTime(levelfolderSceneName, levelSceneName, finalTime);
            levelTimes.UpdateLevelCheckpointTimes(levelfolderSceneName, levelSceneName, showCheckpointTimeScript.checkpointTimes);
        }
    }

    private bool AllCollected() {
        for (int i = 0; i < checkpoints.Length; i++) {
            if (!checkpoints[i].GetComponent<CheckpointScript>().isCollected()) {
                return false;
            }
        }
        return true;
    }

    private void StopPlayer() {
        player.GetComponent<Move>().StopMovement();
        player.GetComponent<Jump>().StopMovement();
        player.GetComponent<GoToLastCheckpoint>().SetFinished();
        player.GetComponent<RespawnScript>().SetFinished();
    }

    private void ActivateEndScreen() {
        endPanel.SetActive(true);
        if (gameMode == null || gameMode.Equals("Singleplayer")) {
            GameObject.Find("MPPostOptions").SetActive(false);
        } else {
            GameObject.Find("SPPostOptions").SetActive(false);
        }
    }

    private void UpdateBingoBoard(float completionTime) {
        if (gameMode != null && gameMode.Equals("Multiplayer")) {
            int bingoIndex = int.Parse(SceneContext.GetElement("BingoIndex"));
            BingoBoardManager bingoManager = GameObject.Find("BoardLogic").GetComponent<BingoBoardManager>();
            bingoManager.SubmitLevelTime(bingoIndex, completionTime);
        }
    }
}

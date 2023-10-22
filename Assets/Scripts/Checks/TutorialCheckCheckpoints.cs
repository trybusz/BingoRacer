using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class TutorialCheckCheckpoints : MonoBehaviour
{
    //public int numCheckpoints;
    public GameObject[] checkpoints;
    private TMP_Text finalTimeText = null;
    public GameObject endPanel;
    private TimeScript timeScript;
    private TutorialShowCheckpointTimeScript tutorialShowCheckpointTimeScript;
    private GameObject player;

    void Start() {
        finalTimeText = GameObject.Find("FinalTimeUI").GetComponent<TMP_Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        timeScript = player.GetComponent<TimeScript>();
        tutorialShowCheckpointTimeScript = player.GetComponent<TutorialShowCheckpointTimeScript>();
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

        //endPanel = GameObject.Find("FinishedLevelCanvas").GetComponent<GameObject>();
        endPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!AllCollected()) {
            return;
        }

        float finalTime = timeScript.GetRunTime();
        string finalTimeString = LevelAssets.ConvertTimeToString(finalTime);

        player.GetComponent<Move>().StopMovement();
        player.GetComponent<Jump>().StopMovement();
        player.GetComponent<TutorialGoToLastCheckpoint>().SetFinished();
        player.GetComponent<RespawnScript>().SetFinished();

        endPanel.SetActive(true);
        finalTimeText.SetText("Time: " + finalTimeString);
    }

    private bool AllCollected() {
        for (int i = 0; i < checkpoints.Length; i++) {
            if (!checkpoints[i].GetComponent<CheckpointScript>().isCollected()) {
                return false;
            }
        }
        return true;
    }
}

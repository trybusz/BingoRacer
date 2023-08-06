using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class CheckCheckpoints : MonoBehaviour
{
    //public int numCheckpoints;
    public GameObject[] checkpoints;
    public int checkpointCounter;
    private TMP_Text finalTimeText = null;
    public GameObject endPanel;
    private TimeScript timeScript;
    private ShowCheckpointTimeScript showCheckpointTimeScript;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        checkpointCounter = 0;
        finalTimeText = GameObject.Find("FinalTimeUI").GetComponent<TMP_Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        timeScript = player.GetComponent<TimeScript>();
        showCheckpointTimeScript = player.GetComponent<ShowCheckpointTimeScript>();
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

        //endPanel = GameObject.Find("FinishedLevelCanvas").GetComponent<GameObject>();
        endPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        for (int i = 0; i < checkpoints.Length; i++) {
            if (checkpoints[i].GetComponent<CheckpointScript>().isCollected()) {
                checkpointCounter++;
            }
        }
        if (checkpointCounter == checkpoints.Length) {
            string levelSceneName = SceneManager.GetActiveScene().name;
            LevelAssets.InitLevelDirectories();
            string levelfolderSceneName = LevelAssets.GetLevelFolderSceneName(levelSceneName);
            LevelTimesData levelTimes = new LevelTimesData();
            float bestTime = levelTimes.GetLevelTime(levelfolderSceneName, levelSceneName);

            float finalTime = timeScript.runTime;
            string finalTimeString = LevelAssets.ConvertTimeToString(finalTime);

            player.GetComponent<Move>().finished = true;
            player.GetComponent<Move>().dashDesired = false;
            player.GetComponent<Move>().inputDirection = 0;
            player.GetComponent<Jump>().finished = true;
            player.GetComponent<Jump>().jumpDesired = false;
            player.GetComponent<Jump>().jumpPressed = false;
            player.GetComponent<GoToLastCheckpoint>().finished = true;
            player.GetComponent<RespawnScript>().finished = true;

            endPanel.SetActive(true);
            finalTimeText.SetText("Time: " + finalTimeString);
            gameObject.GetComponent<DisplayMedalsScript>().displayMedals(finalTime);

            if (finalTime < bestTime) {
                levelTimes.UpdateLevelTime(levelfolderSceneName, levelSceneName, finalTime);
                levelTimes.UpdateLevelCheckpointTimes(levelfolderSceneName, levelSceneName, showCheckpointTimeScript.checkpointTimes);
            }

            //End Game
        }
        else {
            checkpointCounter = 0;
        }
    }

}

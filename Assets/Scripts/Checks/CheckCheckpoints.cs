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

    // Start is called before the first frame update
    void Start()
    {
        checkpointCounter = 0;
        finalTimeText = GameObject.Find("FinalTimeUI").GetComponent<TMP_Text>();
        
        //endPanel = GameObject.Find("FinishedLevelCanvas").GetComponent<GameObject>();
        endPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        for (int i = 0; i < checkpoints.Length; i++) {
            if (checkpoints[i].GetComponent<CheckpointScript>().isCollected()) {
                checkpointCounter++;
            }
        }
        if (checkpointCounter == checkpoints.Length) {
            string levelName = SceneManager.GetActiveScene().name;
            LevelTimesData levelTimes = new LevelTimesData();
            float bestTime = levelTimes.GetLevelTime("OGLevels", levelName);

            float finalTime = GameObject.FindGameObjectWithTag("Player").GetComponent<TimeScript>().finalTime;
            string finalTimeString = LevelAssets.ConvertTimeToString(finalTime);

            GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().finished = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().dashDesired = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().inputDirection = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Jump>().finished = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Jump>().jumpDesired = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Jump>().jumpPressed = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<GoToLastCheckpoint>().finished = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<RespawnScript>().finished = true;

            endPanel.SetActive(true);
            finalTimeText.SetText("Time: " + finalTimeString);
            gameObject.GetComponent<DisplayMedalsScript>().displayMedals(finalTime);

            if (finalTime < bestTime) {
                levelTimes.UpdateLevelTime("OGLevels", levelName, finalTime);
            }

            //End Game
        }
        else {
            checkpointCounter = 0;
        }
    }

}

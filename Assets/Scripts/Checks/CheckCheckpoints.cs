using System.Collections;
using System.Collections.Generic;
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
            float finalTime = GameObject.FindGameObjectWithTag("Player").GetComponent<TimeScript>().finalTime;
            int minutes = (int)finalTime / 60;
            int seconds = (int)finalTime % 60;
            int milliseconds = (int)Mathf.Round((finalTime % 1) * 1000);

            GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().finished = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Jump>().finished = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<GoToLastCheckpoint>().finished = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<RespawnScript>().finished = true;

            endPanel.SetActive(true);
            if (seconds < 10 && minutes < 10) {
                finalTimeText.SetText("Time: " + "0" + minutes + ":0" + seconds + "." + milliseconds);
            }
            else if (seconds < 10) {
                finalTimeText.SetText("Time: " + minutes + ":0" + seconds + "." + milliseconds);
            }
            else if (minutes < 10) {
                finalTimeText.SetText("Time: " + "0" + minutes + ":" + seconds + "." + milliseconds);
            }
            else {
                finalTimeText.SetText("Time: " + minutes + ":" + seconds + "." + milliseconds);
            }



            //End Game
        }
        else {
            checkpointCounter = 0;
        }
    }

}

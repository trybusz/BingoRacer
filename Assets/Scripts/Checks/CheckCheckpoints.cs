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

    // Start is called before the first frame update
    void Start()
    {
        checkpointCounter = 0;
        finalTimeText = GameObject.Find("FinalTimeUI").GetComponent<TMP_Text>();
        finalTimeText.enabled = false;
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
            float seconds = finalTime % 60;


            finalTimeText.enabled = true;
            if (seconds < 10 && minutes < 10) {
                finalTimeText.SetText("Time: " + "0" + minutes + ":0" + seconds);
            }
            else if (seconds < 10) {
                finalTimeText.SetText("Time: " + minutes + ":0" + seconds);
            }
            else if (minutes < 10) {
                finalTimeText.SetText("Time: " + "0" + minutes + ":" + seconds);
            }
            else {
                finalTimeText.SetText("Time: " + minutes + ":" + seconds);
            }



            //End Game
        }
        else {
            checkpointCounter = 0;
        }
    }

}

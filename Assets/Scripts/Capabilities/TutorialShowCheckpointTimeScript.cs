using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialShowCheckpointTimeScript : MonoBehaviour
{
    private TMP_Text checkpointCountText = null;
    public int checkpointCounter;
    public GameObject[] checkpoints;

    void Start()
    {
        // initialize checkpoint text objects
        checkpointCountText = GameObject.Find("CheckpointCounter").GetComponent<TMP_Text>();
        checkpointCounter = 0;
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        DisplayCheckpointCount();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Checkpoint") && other.GetComponent<CheckpointScript>().collected == false) {
            // count and track new collected checkpoint
            checkpointCounter++;
            DisplayCheckpointCount();
            other.GetComponent<CheckpointScript>().collected = true;
        }
    }

    public void DisplayCheckpointCount() {
        checkpointCountText.SetText(checkpointCounter + "/" + checkpoints.Length);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCheckpoints : MonoBehaviour
{
    //public int numCheckpoints;
    public GameObject[] checkpoints;
    public int checkpointCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        checkpointCounter = 0;
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
            //End Game
        }
        else {
            checkpointCounter = 0;
        }
    }

}

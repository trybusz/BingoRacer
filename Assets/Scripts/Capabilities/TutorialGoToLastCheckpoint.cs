using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class TutorialGoToLastCheckpoint : MonoBehaviour
{
    TutorialShowCheckpointTimeScript tutorialShowCheckpointTimeScript = null;
    //[SerializeField] private InputController input = null;
    public Vector3 checkpointPosition;
    public bool collectedCheckpoint;
    public GameObject[] checkpoints;
    //GameObject lastCheckpoint;

    public bool finished;

    //new input stuff
    private PlayerInput playerInput;


    // Start is called before the first frame update
    void Start()
    {
        collectedCheckpoint = false;
        playerInput = GetComponent<PlayerInput>();
        tutorialShowCheckpointTimeScript = gameObject.GetComponent<TutorialShowCheckpointTimeScript>();
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (finished) return;
        //if (input.RetrieveCheckpointInputDown() && collectedCheckpoint) { // Old
        if (playerInput.actions["Checkpoint"].ReadValue<float>() == 1 && collectedCheckpoint) {
            this.gameObject.transform.position = checkpointPosition;
        }
        //if (input.RetrieveSpawnInputDown()) { // Old
        if (playerInput.actions["Restart"].ReadValue<float>() == 1) {
            collectedCheckpoint = false;
            tutorialShowCheckpointTimeScript.checkpointCounter = 0;
            tutorialShowCheckpointTimeScript.DisplayCheckpointCount();

            for (int i = 0; i < checkpoints.Length; i++) {
                if (checkpoints[i].GetComponent<CheckpointScript>().isCollected()) {
                    checkpoints[i].GetComponent<CheckpointScript>().setUncollected();
                }
            }
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Checkpoint")) {
            checkpointPosition = collision.transform.position;
            collectedCheckpoint = true;
        }
    }
}

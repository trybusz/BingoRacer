using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GoToLastCheckpoint : MonoBehaviour
{
    //[SerializeField] private InputController input = null;
    public Vector3 checkpointPosition;
    public bool collectedCheckpoint;
    public GameObject[] checkpoints;
    //GameObject lastCheckpoint;

    //new input stuff
    private PlayerActionControls playerActionControls;

    private void Awake() {
        playerActionControls = new PlayerActionControls();
    }

    private void OnEnable() {
        playerActionControls.Enable();
    }

    private void OnDisable() {
        playerActionControls.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        collectedCheckpoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (input.RetrieveCheckpointInputDown() && collectedCheckpoint) { // Old
        if (playerActionControls.Game.Checkpoint.ReadValue<float>() == 1 && collectedCheckpoint) {
            this.gameObject.transform.position = checkpointPosition;
        } 
        //if (input.RetrieveSpawnInputDown()) { // Old
        if (playerActionControls.Game.Restart.ReadValue<float>() == 1) {
                collectedCheckpoint = false;
            
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
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

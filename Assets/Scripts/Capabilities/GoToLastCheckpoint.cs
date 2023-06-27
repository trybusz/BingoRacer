using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLastCheckpoint : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    public Vector3 checkpointPosition;
    public bool collectedCheckpoint;
    //GameObject lastCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        collectedCheckpoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (input.RetrieveCheckpointInputDown() && collectedCheckpoint) {
            this.gameObject.transform.position = checkpointPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Checkpoint")) {
            checkpointPosition = collision.transform.position;
            collectedCheckpoint = true;
        }
    }
}

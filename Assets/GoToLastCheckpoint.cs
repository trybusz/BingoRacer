using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLastCheckpoint : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    GameObject lastCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (input.RetrieveCheckpointInputDown()) {
            this.gameObject.transform.position = lastCheckpoint.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Checkpoint")) {
            lastCheckpoint = collision.GetComponent<GameObject>();
        }
    }
}

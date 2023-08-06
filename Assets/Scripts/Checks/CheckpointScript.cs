using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public bool collected;
    // Start is called before the first frame update
    void Start()
    {
        collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            collected = true;
        }
    }*/

    public void setUncollected() {
        collected = false;
    }

    public bool isCollected() {
        return collected;
    }
}

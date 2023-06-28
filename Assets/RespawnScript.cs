using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    public Vector3 spawnPosition;
    //GameObject lastCheckpoint;

    // Start is called before the first frame update
    void Start() {
        spawnPosition = GameObject.FindGameObjectWithTag("Start").transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (input.RetrieveSpawnInputDown()) {
            this.gameObject.transform.position = spawnPosition;
        }
    }
}

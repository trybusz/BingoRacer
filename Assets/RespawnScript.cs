using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnScript : MonoBehaviour
{
    //[SerializeField] private InputController input = null;
    public Vector3 spawnPosition;
    //GameObject lastCheckpoint;

    //new input stuff
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start() {
        spawnPosition = GameObject.FindGameObjectWithTag("Start").transform.position;
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update() {
        //if (input.RetrieveSpawnInputDown()) { // Old
        if (playerInput.actions["Restart"].ReadValue<float>() == 1) {
                this.gameObject.transform.position = spawnPosition;
        }
    }
}

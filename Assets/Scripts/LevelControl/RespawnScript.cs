using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnScript : MonoBehaviour
{
    TimeScript timeScript = null;
    //[SerializeField] private InputController input = null;
    public Vector3 spawnPosition;
    //GameObject lastCheckpoint;

    private bool finished;

    private PlayerInput playerInput;

    private void Start() {
        spawnPosition = GameObject.FindGameObjectWithTag("Start").transform.position;
        playerInput = GetComponent<PlayerInput>();
        timeScript = gameObject.GetComponent<TimeScript>();
    }

    private void Update() {
        //if (input.RetrieveSpawnInputDown()) { // Old
        if (!finished && playerInput.actions["Restart"].ReadValue<float>() == 1) {
            gameObject.transform.position = spawnPosition;
            timeScript.RestartTime();
        }
    }

    public void SetFinished() {
        finished = true;
    }
}

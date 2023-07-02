using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    //[SerializeField] private InputController input = null;
    public Vector3 spawnPosition;
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
    void Start() {
        spawnPosition = GameObject.FindGameObjectWithTag("Start").transform.position;
    }

    // Update is called once per frame
    void Update() {
        //if (input.RetrieveSpawnInputDown()) { // Old
        if (playerActionControls.Game.Restart.ReadValue<float>() == 1) {
                this.gameObject.transform.position = spawnPosition;
        }
    }
}

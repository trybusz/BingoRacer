using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneScript : MonoBehaviour {
    //[SerializeField] private InputController input = null;

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
        
    }

    // Update is called once per frame
    void Update() {
        //if (input.ReloadLevelInputDown()) { // Old
        if (playerActionControls.Game.ReloadSceneTemp.ReadValue<float>() == 1) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

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

    void Update() {
        //if (input.ReloadLevelInputDown()) { // Old
        if (playerActionControls.Game.ReloadSceneTemp.ReadValue<float>() == 1) {
            string currentLevelSceneName = SceneContext.GetElement("Level");
            SceneManager.LoadScene(currentLevelSceneName);
        }
    }
}

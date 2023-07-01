using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneScript : MonoBehaviour {
    [SerializeField] private InputController input = null;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (input.ReloadLevelInputDown()) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

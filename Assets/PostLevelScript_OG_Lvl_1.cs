using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostLevelScript_OG_Lvl_1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void backToLevelSelect() {
        SceneManager.LoadScene("OG_Lvl_Select");
    }

    void restartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void nextLevel() {
        SceneManager.LoadScene("OG_Lvl_2");

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void backToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void toOGLevelSelect() {
        SceneManager.LoadScene("OG_Lvl_Select");
    }
}
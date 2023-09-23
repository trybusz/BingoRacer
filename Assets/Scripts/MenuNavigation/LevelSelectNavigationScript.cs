using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectNavigationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void toSingleplayerMenu() {
        SceneManager.LoadScene("SingleplayerMenu");
    }

    public void toLevel(string levelSceneName) {
        SceneManager.LoadScene(levelSceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OGSelectButtonsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void toLevelSelector() {
        SceneManager.LoadScene("LevelSelector");
    }
    public void toLevel1() {
        SceneManager.LoadScene("OG_Lvl_1");
    }
    public void toLevel2() {
        SceneManager.LoadScene("OG_Lvl_2");
    }
    public void toLevel3() {
        SceneManager.LoadScene("OG_Lvl_3");
    }


}

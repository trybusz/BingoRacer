using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayMedalsScript : MonoBehaviour
{
    private Image bronzeMedal = null;
    private Image silverMedal = null;
    private Image goldMedal = null;
    private Image makerMedal = null;
    private float makerMedalTime;
    private float goldMedalTime;
    private float silverMedalTime;
    private float bronzeMedalTime;
    private string levelSceneName;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayMedals(float playerTime) {
        levelSceneName = SceneManager.GetActiveScene().name;
        bronzeMedal = GameObject.Find("BronzeMedal").GetComponent<Image>();
        silverMedal = GameObject.Find("SilverMedal").GetComponent<Image>();
        goldMedal = GameObject.Find("GoldMedal").GetComponent<Image>();
        makerMedal = GameObject.Find("MakerMedal").GetComponent<Image>();
        bronzeMedal.enabled = false;
        silverMedal.enabled = false;
        goldMedal.enabled = false;
        makerMedal.enabled = false;
        // TODO: only call this once, but make sure it is called before the data will be used.
        LevelAssets.InitLevelDirectories();
        makerMedalTime = LevelAssets.GetLevelMakerTime(levelSceneName);
        goldMedalTime = Mathf.Ceil(makerMedalTime * 1.1f);
        silverMedalTime = Mathf.Ceil(makerMedalTime * 1.25f);
        bronzeMedalTime = Mathf.Ceil(makerMedalTime * 1.5f);
        bronzeMedal.enabled = playerTime <= bronzeMedalTime;
        silverMedal.enabled = playerTime <= silverMedalTime;
        goldMedal.enabled = playerTime <= goldMedalTime;
        makerMedal.enabled = playerTime <= makerMedalTime;
    }

    public void findTimes() {
        
        if (levelSceneName == "OG_Lvl_1") {
            makerMedalTime = 26.999f; //Make Sure to Set both here and in menus
            goldMedalTime = 30f;   //Maker Medal Time Times 1.1 rounded up to second
            silverMedalTime = 34f; //Maker Medal Time Times 1.25 rounded up to second
            bronzeMedalTime = 41f; //Maker Medal Time Times 1.5 rounded up to second
        }
        else if (levelSceneName == "OG_Lvl_2") {
            makerMedalTime = 29.956f; //Make Sure to Set both here and in menus
            goldMedalTime = 33;   //Maker Medal Time Times 1.1 rounded up to second
            silverMedalTime = 38; //Maker Medal Time Times 1.25 rounded up to second
            bronzeMedalTime = 45; //Maker Medal Time Times 1.5 rounded up to second
        }
        else if (levelSceneName == "OG_Lvl_6") {
            makerMedalTime = 27.316f; //Make Sure to Set both here and in menus
            goldMedalTime = 31;   //Maker Medal Time Times 1.1 rounded up to second
            silverMedalTime = 35; //Maker Medal Time Times 1.25 rounded up to second
            bronzeMedalTime = 41; //Maker Medal Time Times 1.5 rounded up to second
        }
        else if (levelSceneName == "OG_Lvl_12") {
            makerMedalTime = 26.756f; //Make Sure to Set both here and in menus
            goldMedalTime = 30;   //Maker Medal Time Times 1.1 rounded up to second
            silverMedalTime = 34; //Maker Medal Time Times 1.25 rounded up to second
            bronzeMedalTime = 41; //Maker Medal Time Times 1.5 rounded up to second
        }
        else if (levelSceneName == "OG_Lvl_18") {
            makerMedalTime = 29.215f; //Make Sure to Set both here and in menus
            goldMedalTime = 33;   //Maker Medal Time Times 1.1 rounded up to second
            silverMedalTime = 37; //Maker Medal Time Times 1.25 rounded up to second
            bronzeMedalTime = 44; //Maker Medal Time Times 1.5 rounded up to second
        }

    }

}

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
    private string levelName;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayMedals(float playerTime) {
        levelName = SceneManager.GetActiveScene().name;
        Debug.Log(levelName);
        bronzeMedal = GameObject.Find("BronzeMedal").GetComponent<Image>();
        silverMedal = GameObject.Find("SilverMedal").GetComponent<Image>();
        goldMedal = GameObject.Find("GoldMedal").GetComponent<Image>();
        makerMedal = GameObject.Find("MakerMedal").GetComponent<Image>();
        bronzeMedal.enabled = false;
        silverMedal.enabled = false;
        goldMedal.enabled = false;
        makerMedal.enabled = false;
        findTimes();
        if (playerTime < bronzeMedalTime) {
            bronzeMedal.enabled = true;
        }
        if (playerTime < silverMedalTime) {
            silverMedal.enabled = true;
        }
        if (playerTime < goldMedalTime) {
            goldMedal.enabled = true;
        }
        if (playerTime < makerMedalTime) {
            makerMedal.enabled = true;
        }
    }

    public void findTimes() {
        if (levelName == "OG_Lvl_1") {
            makerMedalTime = 27.316f; //Make Sure to Set both here and in menus
            goldMedalTime = 31;   //Maker Medal Time Times 1.1 rounded up to second
            silverMedalTime = 35; //Maker Medal Time Times 1.25 rounded up to second
            bronzeMedalTime = 41; //Maker Medal Time Times 1.5 rounded up to second
        }
        else if (levelName == "OG_Lvl_2") {
            makerMedalTime = 26.756f; //Make Sure to Set both here and in menus
            goldMedalTime = 30;   //Maker Medal Time Times 1.1 rounded up to second
            silverMedalTime = 34; //Maker Medal Time Times 1.25 rounded up to second
            bronzeMedalTime = 41; //Maker Medal Time Times 1.5 rounded up to second
        }
        else if (levelName == "OG_Lvl_3") {
            makerMedalTime = 29.215f; //Make Sure to Set both here and in menus
            goldMedalTime = 33;   //Maker Medal Time Times 1.1 rounded up to second
            silverMedalTime = 37; //Maker Medal Time Times 1.25 rounded up to second
            bronzeMedalTime = 44; //Maker Medal Time Times 1.5 rounded up to second
        }
    }

}

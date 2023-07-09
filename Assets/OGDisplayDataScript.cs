using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OGDisplayDataScript : MonoBehaviour
{
    private TMP_Text levelText = null;
    private TMP_Text bestTimeText = null;
    private Image bronzeMedal = null;
    private Image silverMedal = null;
    private Image goldMedal = null;
    private Image makerMedal = null;
    private TMP_Text makerTime = null;
    private TMP_Text goldTime = null;
    private TMP_Text silverTime = null;
    private TMP_Text bronzeTime = null;

    public int minutes;
    public float seconds;

    private float playerBestTime;
    private float makerMedalTime;
    private float goldMedalTime;
    private float silverMedalTime;
    private float bronzeMedalTime;
    // Start is called before the first frame update
    void Start()
    {
        levelText = GameObject.Find("LevelNumber").GetComponent<TMP_Text>();
        bestTimeText = GameObject.Find("Best Time").GetComponent<TMP_Text>();
        bronzeMedal = GameObject.Find("BronzeMedal").GetComponent<Image>();
        silverMedal = GameObject.Find("SilverMedal").GetComponent<Image>();
        goldMedal = GameObject.Find("GoldMedal").GetComponent<Image>();
        makerMedal = GameObject.Find("MakerMedal").GetComponent<Image>();
        makerTime = GameObject.Find("Maker Time").GetComponent<TMP_Text>();
        goldTime = GameObject.Find("Gold Time").GetComponent<TMP_Text>();
        silverTime = GameObject.Find("Silver Time").GetComponent<TMP_Text>();
        bronzeTime = GameObject.Find("Bronze Time").GetComponent<TMP_Text>();
        showLevel1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showLevel1() {
        bronzeMedal.enabled = false;
        silverMedal.enabled = false;
        goldMedal.enabled = false;
        makerMedal.enabled = false;
        levelText.SetText("Level 1");
        playerBestTime = 0;//Phillip get this
        minutes = (int)playerBestTime / 60;
        seconds = playerBestTime % 60;
        seconds = Mathf.Round(seconds * 1000f) / 1000f;
        if (seconds < 10 && minutes < 10) {
            bestTimeText.SetText("Best Time: " + "0" + minutes + ":0" + seconds);
        }
        else if (seconds < 10) {
            bestTimeText.SetText("Best Time: " + minutes + ":0" + seconds);
        }
        else if (minutes < 10) {
            bestTimeText.SetText("Best Time: " + "0" + minutes + ":" + seconds);
        }
        else {
            bestTimeText.SetText("Best Time: " + minutes + ":" + seconds);
        }
        makerMedalTime = 27.316f; //Make Sure to Set both here and in level
        goldMedalTime = 31;   //Maker Medal Time Times 1.1 rounded up to second
        silverMedalTime = 35; //Maker Medal Time Times 1.25 rounded up to second
        bronzeMedalTime = 41; //Maker Medal Time Times 1.5 rounded up to second
        if (playerBestTime < bronzeMedalTime) {
            bronzeMedal.enabled = true;
        }
        if (playerBestTime < silverMedalTime) {
            silverMedal.enabled = true;
        }
        if (playerBestTime < goldMedalTime) {
            goldMedal.enabled = true;
        }
        if (playerBestTime < makerMedalTime) {
            makerMedal.enabled = true;
        }
        makerTime.SetText("Maker Time: 00:27.497");
        goldTime.SetText("Gold Time: 00:30.000");
        silverTime.SetText("Silver Time: 00:33.000");
        bronzeTime.SetText("Bronze Time: 00:42.000");
    }
    public void showLevel2() {
        bronzeMedal.enabled = false;
        silverMedal.enabled = false;
        goldMedal.enabled = false;
        makerMedal.enabled = false;
        levelText.SetText("Level 2");
        playerBestTime = 0;//Phillip get this
        minutes = (int)playerBestTime / 60;
        seconds = playerBestTime % 60;
        seconds = Mathf.Round(seconds * 1000f) / 1000f;
        if (seconds < 10 && minutes < 10) {
            bestTimeText.SetText("Best Time: " + "0" + minutes + ":0" + seconds);
        }
        else if (seconds < 10) {
            bestTimeText.SetText("Best Time: " + minutes + ":0" + seconds);
        }
        else if (minutes < 10) {
            bestTimeText.SetText("Best Time: " + "0" + minutes + ":" + seconds);
        }
        else {
            bestTimeText.SetText("Best Time: " + minutes + ":" + seconds);
        }
        makerMedalTime = 26.756f;
        goldMedalTime = 30;   //Maker Medal Time Times 1.1 rounded up to second
        silverMedalTime = 34; //Maker Medal Time Times 1.25 rounded up to second
        bronzeMedalTime = 41; //Maker Medal Time Times 1.5 rounded up to second
        if (playerBestTime < bronzeMedalTime) {
            bronzeMedal.enabled = true;
        }
        if (playerBestTime < silverMedalTime) {
            silverMedal.enabled = true;
        }
        if (playerBestTime < goldMedalTime) {
            goldMedal.enabled = true;
        }
        if (playerBestTime < makerMedalTime) {
            makerMedal.enabled = true;
        }
        makerTime.SetText("Maker Time: 00:26.756");
        goldTime.SetText("Gold Time: 00:29.000");
        silverTime.SetText("Silver Time: 00:33.000");
        bronzeTime.SetText("Bronze Time: 00:41.000");
    }
    public void showLevel3() { //////////
        bronzeMedal.enabled = false;
        silverMedal.enabled = false;
        goldMedal.enabled = false;
        makerMedal.enabled = false;
        levelText.SetText("Level 3"); //////////
        playerBestTime = 0;//Phillip get this
        minutes = (int)playerBestTime / 60;
        seconds = playerBestTime % 60;
        seconds = Mathf.Round(seconds * 1000f) / 1000f;
        if (seconds < 10 && minutes < 10) {
            bestTimeText.SetText("Best Time: " + "0" + minutes + ":0" + seconds);
        }
        else if (seconds < 10) {
            bestTimeText.SetText("Best Time: " + minutes + ":0" + seconds);
        }
        else if (minutes < 10) {
            bestTimeText.SetText("Best Time: " + "0" + minutes + ":" + seconds);
        }
        else {
            bestTimeText.SetText("Best Time: " + minutes + ":" + seconds);
        }
        makerMedalTime = 29.215f; //////////
        goldMedalTime = 33;   //Maker Medal Time Times 1.1 rounded up to second //////////
        silverMedalTime = 37; //Maker Medal Time Times 1.25 rounded up to second //////////
        bronzeMedalTime = 44; //Maker Medal Time Times 1.5 rounded up to second //////////
        if (playerBestTime < bronzeMedalTime) {
            bronzeMedal.enabled = true;
        }
        if (playerBestTime < silverMedalTime) {
            silverMedal.enabled = true;
        }
        if (playerBestTime < goldMedalTime) {
            goldMedal.enabled = true;
        }
        if (playerBestTime < makerMedalTime) {
            makerMedal.enabled = true;
        }
        makerTime.SetText("Maker Time: 00:29.215"); //////////
        goldTime.SetText("Gold Time: 00:32.000"); //////////
        silverTime.SetText("Silver Time: 00:36.000"); //////////
        bronzeTime.SetText("Bronze Time: 00:44.000"); //////////
    }
}

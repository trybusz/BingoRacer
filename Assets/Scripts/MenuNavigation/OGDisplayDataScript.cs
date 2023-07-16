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

    LevelTimesData levelTimes;

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
        levelTimes = new LevelTimesData();
        ShowLevelData("OG_Lvl_1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLevelData(string levelSceneName) {
        // TODO: only call this once, but make sure it is called before the data will be used.
        LevelAssets.InitLevelDirectories();
        bronzeMedal.enabled = false;
        silverMedal.enabled = false;
        goldMedal.enabled = false;
        makerMedal.enabled = false;
        string levelDisplayName = LevelAssets.GetLevelDisplayName(levelSceneName);
        string GetLevelFolderDisplayName = LevelAssets.GetLevelFolderDisplayName(levelSceneName);
        levelText.SetText(levelDisplayName);
        playerBestTime = levelTimes.GetLevelTime(GetLevelFolderDisplayName, levelSceneName);
        string playerBestTimeString = LevelAssets.ConvertTimeToString(playerBestTime);
        bestTimeText.SetText("Best Time: " + playerBestTimeString);

        makerMedalTime = LevelAssets.GetLevelMakerTime(levelSceneName); // Make Sure to Set both here and in level
        goldMedalTime = Mathf.Ceil(makerMedalTime * 1.1f);
        silverMedalTime = Mathf.Ceil(makerMedalTime * 1.25f);
        bronzeMedalTime = Mathf.Ceil(makerMedalTime * 1.5f);
        bronzeMedal.enabled = playerBestTime <= bronzeMedalTime;
        silverMedal.enabled = playerBestTime <= silverMedalTime;
        goldMedal.enabled = playerBestTime <= goldMedalTime;
        makerMedal.enabled = playerBestTime <= makerMedalTime;
        makerTime.SetText("Maker Time: " + LevelAssets.ConvertTimeToString(makerMedalTime));
        goldTime.SetText("Gold Time: " + LevelAssets.ConvertTimeToString(goldMedalTime));
        silverTime.SetText("Silver Time: " + LevelAssets.ConvertTimeToString(silverMedalTime));
        bronzeTime.SetText("Bronze Time: " + LevelAssets.ConvertTimeToString(bronzeMedalTime));
    }

    // public void showLevel1() {
    //     bronzeMedal.enabled = false;
    //     silverMedal.enabled = false;
    //     goldMedal.enabled = false;
    //     makerMedal.enabled = false;
    //     levelText.SetText("Level 1");
    //     playerBestTime = levelTimes.GetLevelTime("OGLevels", "OG_Lvl_1");
    //     minutes = (int)playerBestTime / 60;
    //     seconds = playerBestTime % 60;
    //     seconds = Mathf.Round(seconds * 1000f) / 1000f;
    //     if (seconds < 10 && minutes < 10) {
    //         bestTimeText.SetText("Best Time: " + "0" + minutes + ":0" + seconds);
    //     }
    //     else if (seconds < 10) {
    //         bestTimeText.SetText("Best Time: " + minutes + ":0" + seconds);
    //     }
    //     else if (minutes < 10) {
    //         bestTimeText.SetText("Best Time: " + "0" + minutes + ":" + seconds);
    //     }
    //     else {
    //         bestTimeText.SetText("Best Time: " + minutes + ":" + seconds);
    //     }
    //     makerMedalTime = 26.999f; //Make Sure to Set both here and in level
    //     goldMedalTime = 30f;   //Maker Medal Time Times 1.1 rounded up to second
    //     silverMedalTime = 34f; //Maker Medal Time Times 1.25 rounded up to second
    //     bronzeMedalTime = 41f; //Maker Medal Time Times 1.5 rounded up to second
    //     if (playerBestTime < bronzeMedalTime) {
    //         bronzeMedal.enabled = true;
    //     }
    //     if (playerBestTime < silverMedalTime) {
    //         silverMedal.enabled = true;
    //     }
    //     if (playerBestTime < goldMedalTime) {
    //         goldMedal.enabled = true;
    //     }
    //     if (playerBestTime < makerMedalTime) {
    //         makerMedal.enabled = true;
    //     }
    //     makerTime.SetText("Maker Time: 00:26.999");
    //     goldTime.SetText("Gold Time: 00:30.000");
    //     silverTime.SetText("Silver Time: 00:34.000");
    //     bronzeTime.SetText("Bronze Time: 00:41.000");
    // }
    // public void showLevel2() {
    //     bronzeMedal.enabled = false;
    //     silverMedal.enabled = false;
    //     goldMedal.enabled = false;
    //     makerMedal.enabled = false;
    //     levelText.SetText("Level 2");
    //     playerBestTime = levelTimes.GetLevelTime("OGLevels", "OG_Lvl_2");
    //     minutes = (int)playerBestTime / 60;
    //     seconds = playerBestTime % 60;
    //     seconds = Mathf.Round(seconds * 1000f) / 1000f;
    //     if (seconds < 10 && minutes < 10) {
    //         bestTimeText.SetText("Best Time: " + "0" + minutes + ":0" + seconds);
    //     }
    //     else if (seconds < 10) {
    //         bestTimeText.SetText("Best Time: " + minutes + ":0" + seconds);
    //     }
    //     else if (minutes < 10) {
    //         bestTimeText.SetText("Best Time: " + "0" + minutes + ":" + seconds);
    //     }
    //     else {
    //         bestTimeText.SetText("Best Time: " + minutes + ":" + seconds);
    //     }
    //     makerMedalTime = 29.956f; //Make Sure to Set both here and in level
    //     goldMedalTime = 33f;   //Maker Medal Time Times 1.1 rounded up to second
    //     silverMedalTime = 38f; //Maker Medal Time Times 1.25 rounded up to second
    //     bronzeMedalTime = 45f; //Maker Medal Time Times 1.5 rounded up to second
    //     if (playerBestTime < bronzeMedalTime) {
    //         bronzeMedal.enabled = true;
    //     }
    //     if (playerBestTime < silverMedalTime) {
    //         silverMedal.enabled = true;
    //     }
    //     if (playerBestTime < goldMedalTime) {
    //         goldMedal.enabled = true;
    //     }
    //     if (playerBestTime < makerMedalTime) {
    //         makerMedal.enabled = true;
    //     }
    //     makerTime.SetText("Maker Time: 00:29.956");
    //     goldTime.SetText("Gold Time: 00:33.000");
    //     silverTime.SetText("Silver Time: 00:38.000");
    //     bronzeTime.SetText("Bronze Time: 00:45.000");
    // }
    // public void showLevel12() {
    //     bronzeMedal.enabled = false;
    //     silverMedal.enabled = false;
    //     goldMedal.enabled = false;
    //     makerMedal.enabled = false;
    //     levelText.SetText("Level 12");
    //     playerBestTime = levelTimes.GetLevelTime("OGLevels", "OG_Lvl_12");
    //     minutes = (int)playerBestTime / 60;
    //     seconds = playerBestTime % 60;
    //     seconds = Mathf.Round(seconds * 1000f) / 1000f;
    //     if (seconds < 10 && minutes < 10) {
    //         bestTimeText.SetText("Best Time: " + "0" + minutes + ":0" + seconds);
    //     }
    //     else if (seconds < 10) {
    //         bestTimeText.SetText("Best Time: " + minutes + ":0" + seconds);
    //     }
    //     else if (minutes < 10) {
    //         bestTimeText.SetText("Best Time: " + "0" + minutes + ":" + seconds);
    //     }
    //     else {
    //         bestTimeText.SetText("Best Time: " + minutes + ":" + seconds);
    //     }
    //     makerMedalTime = 26.756f;
    //     goldMedalTime = 30f;   //Maker Medal Time Times 1.1 rounded up to second
    //     silverMedalTime = 34f; //Maker Medal Time Times 1.25 rounded up to second
    //     bronzeMedalTime = 41f; //Maker Medal Time Times 1.5 rounded up to second
    //     if (playerBestTime < bronzeMedalTime) {
    //         bronzeMedal.enabled = true;
    //     }
    //     if (playerBestTime < silverMedalTime) {
    //         silverMedal.enabled = true;
    //     }
    //     if (playerBestTime < goldMedalTime) {
    //         goldMedal.enabled = true;
    //     }
    //     if (playerBestTime < makerMedalTime) {
    //         makerMedal.enabled = true;
    //     }
    //     makerTime.SetText("Maker Time: 00:26.756");
    //     goldTime.SetText("Gold Time: 00:30.000");
    //     silverTime.SetText("Silver Time: 00:34.000");
    //     bronzeTime.SetText("Bronze Time: 00:41.000");
    // }
    // public void showLevel18() { //////////
    //     bronzeMedal.enabled = false;
    //     silverMedal.enabled = false;
    //     goldMedal.enabled = false;
    //     makerMedal.enabled = false;
    //     levelText.SetText("Level 18"); //////////
    //     playerBestTime = levelTimes.GetLevelTime("OGLevels", "OG_Lvl_18");
    //     minutes = (int)playerBestTime / 60;
    //     seconds = playerBestTime % 60;
    //     seconds = Mathf.Round(seconds * 1000f) / 1000f;
    //     if (seconds < 10 && minutes < 10) {
    //         bestTimeText.SetText("Best Time: " + "0" + minutes + ":0" + seconds);
    //     }
    //     else if (seconds < 10) {
    //         bestTimeText.SetText("Best Time: " + minutes + ":0" + seconds);
    //     }
    //     else if (minutes < 10) {
    //         bestTimeText.SetText("Best Time: " + "0" + minutes + ":" + seconds);
    //     }
    //     else {
    //         bestTimeText.SetText("Best Time: " + minutes + ":" + seconds);
    //     }
    //     makerMedalTime = 29.215f; //////////
    //     goldMedalTime = 33f;   //Maker Medal Time Times 1.1 rounded up to second //////////
    //     silverMedalTime = 37f; //Maker Medal Time Times 1.25 rounded up to second //////////
    //     bronzeMedalTime = 44f; //Maker Medal Time Times 1.5 rounded up to second //////////
    //     if (playerBestTime < bronzeMedalTime) {
    //         bronzeMedal.enabled = true;
    //     }
    //     if (playerBestTime < silverMedalTime) {
    //         silverMedal.enabled = true;
    //     }
    //     if (playerBestTime < goldMedalTime) {
    //         goldMedal.enabled = true;
    //     }
    //     if (playerBestTime < makerMedalTime) {
    //         makerMedal.enabled = true;
    //     }
    //     makerTime.SetText("Maker Time: 00:29.215"); //////////
    //     goldTime.SetText("Gold Time: 00:33.000"); //////////
    //     silverTime.SetText("Silver Time: 00:37.000"); //////////
    //     bronzeTime.SetText("Bronze Time: 00:44.000"); //////////
    // }
    // public void showLevel6() {
    //     bronzeMedal.enabled = false;
    //     silverMedal.enabled = false;
    //     goldMedal.enabled = false;
    //     makerMedal.enabled = false;
    //     levelText.SetText("Level 6");
    //     playerBestTime = levelTimes.GetLevelTime("OGLevels", "OG_Lvl_6");
    //     minutes = (int)playerBestTime / 60;
    //     seconds = playerBestTime % 60;
    //     seconds = Mathf.Round(seconds * 1000f) / 1000f;
    //     if (seconds < 10 && minutes < 10) {
    //         bestTimeText.SetText("Best Time: " + "0" + minutes + ":0" + seconds);
    //     }
    //     else if (seconds < 10) {
    //         bestTimeText.SetText("Best Time: " + minutes + ":0" + seconds);
    //     }
    //     else if (minutes < 10) {
    //         bestTimeText.SetText("Best Time: " + "0" + minutes + ":" + seconds);
    //     }
    //     else {
    //         bestTimeText.SetText("Best Time: " + minutes + ":" + seconds);
    //     }
    //     makerMedalTime = 27.316f; //Make Sure to Set both here and in level
    //     goldMedalTime = 31f;   //Maker Medal Time Times 1.1 rounded up to second
    //     silverMedalTime = 35f; //Maker Medal Time Times 1.25 rounded up to second
    //     bronzeMedalTime = 41f; //Maker Medal Time Times 1.5 rounded up to second
    //     if (playerBestTime < bronzeMedalTime) {
    //         bronzeMedal.enabled = true;
    //     }
    //     if (playerBestTime < silverMedalTime) {
    //         silverMedal.enabled = true;
    //     }
    //     if (playerBestTime < goldMedalTime) {
    //         goldMedal.enabled = true;
    //     }
    //     if (playerBestTime < makerMedalTime) {
    //         makerMedal.enabled = true;
    //     }
    //     makerTime.SetText("Maker Time: 00:27.497");
    //     goldTime.SetText("Gold Time: 00:31.000");
    //     silverTime.SetText("Silver Time: 00:35.000");
    //     bronzeTime.SetText("Bronze Time: 00:41.000");
    // }
}

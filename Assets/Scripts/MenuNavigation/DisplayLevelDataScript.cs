using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayLevelDataScript : MonoBehaviour
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
        string levelFolderSceneName = LevelAssets.GetLevelFolderSceneName(levelSceneName);
        levelText.SetText(levelDisplayName);
        playerBestTime = levelTimes.GetLevelTime(levelFolderSceneName, levelSceneName);
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
}

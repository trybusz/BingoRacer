using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayMedalsScript : MonoBehaviour {
    public void displayMedals(float playerTime) {
        // TODO: only call this once, but make sure it is called before the data will be used.
        string levelSceneName = SceneManager.GetActiveScene().name;
        float makerMedalTime = LevelAssets.GetLevelMakerTime(levelSceneName);
        float goldMedalTime = Mathf.Ceil(makerMedalTime * 1.1f);
        float silverMedalTime = Mathf.Ceil(makerMedalTime * 1.25f);
        float bronzeMedalTime = Mathf.Ceil(makerMedalTime * 1.5f);

        Image bronzeMedal = GameObject.Find("BronzeMedal").GetComponent<Image>();
        bronzeMedal.enabled = playerTime <= bronzeMedalTime;
        Image silverMedal = GameObject.Find("SilverMedal").GetComponent<Image>();
        silverMedal.enabled = playerTime <= silverMedalTime;
        Image goldMedal = GameObject.Find("GoldMedal").GetComponent<Image>();
        goldMedal.enabled = playerTime <= goldMedalTime;
        Image makerMedal = GameObject.Find("MakerMedal").GetComponent<Image>();
        makerMedal.enabled = playerTime <= makerMedalTime;
    }
}

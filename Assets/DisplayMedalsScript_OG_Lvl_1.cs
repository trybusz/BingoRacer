using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMedalsScript_OG_Lvl_1 : MonoBehaviour {
    private Image bronzeMedal = null;
    private Image silverMedal = null;
    private Image goldMedal = null;
    private Image makerMedal = null;
    private float makerMedalTime;
    private float goldMedalTime;
    private float silverMedalTime;
    private float bronzeMedalTime;
    // Start is called before the first frame update
    void Start() {
        bronzeMedal = GameObject.Find("BronzeMedal").GetComponent<Image>();
        silverMedal = GameObject.Find("SilverMedal").GetComponent<Image>();
        goldMedal = GameObject.Find("GoldMedal").GetComponent<Image>();
        makerMedal = GameObject.Find("MakerMedal").GetComponent<Image>();
        makerMedalTime = 27.497f; //Make Sure to Set both here and in menus
        goldMedalTime = 30;   //Maker Medal Time Times 1.065 rounded up to second
        silverMedalTime = 33; //Maker Medal Time Times 1.2 rounded up to second
        bronzeMedalTime = 42; //Maker Medal Time Times 1.5 rounded up to second
    }

    // Update is called once per frame
    void Update() {

    }

    public void displayMedals(float playerTime) {
        bronzeMedal.enabled = false;
        silverMedal.enabled = false;
        goldMedal.enabled = false;
        makerMedal.enabled = false;
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
}
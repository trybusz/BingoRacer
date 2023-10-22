using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    private TMP_Text timeText = null;
    
    private bool leftStart;
    private float timeOfStart;
    private float runTime;

    void Start() {
        leftStart = false;
        timeText = GameObject.Find("TimeUI").GetComponent<TMP_Text>();
    }

    void Update() {
        if (!leftStart) {
            timeText.SetText("00:00.000");
        }
        else {
            runTime = Time.timeSinceLevelLoad - timeOfStart;
            timeText.SetText(LevelAssets.ConvertTimeToString(runTime));
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Start") && !leftStart) {
            timeOfStart = Time.timeSinceLevelLoad;
            leftStart = true;
        }
    }

    public float GetRunTime() {
        return runTime;
    }

    public void RestartTime() {
        leftStart = false;
    }
}

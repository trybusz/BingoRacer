using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    private TMP_Text timeText = null;
    
    public bool inStart;
    public float timeOfStart;
    public int minutes;
    public float seconds;
    public float runTime;
    public float finalTime;

    // Start is called before the first frame update
    void Start()
    {
        inStart = true;

        timeText = GameObject.Find("TimeUI").GetComponent<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inStart) {
            timeText.SetText("00:00.000");
        }
        else {
            runTime = Time.timeSinceLevelLoad - timeOfStart;
            minutes = (int)runTime / 60;
            seconds = runTime % 60;
            seconds = Mathf.Round(seconds * 1000f) / 1000f;
            if(seconds < 10 && minutes < 10) {
                timeText.SetText("0" + minutes + ":0" + seconds);
            }
            else if (seconds < 10) {
                timeText.SetText(minutes + ":0" + seconds);
            }
            else if (minutes < 10) {
                timeText.SetText("0" + minutes + ":" + seconds);
            }
            else {
                timeText.SetText(minutes + ":" + seconds);
            }
        }
        finalTime = minutes * 60 + seconds;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Start")) {
            inStart = true;
        }
        

    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Start")) {
            inStart = false;
            timeOfStart = Time.timeSinceLevelLoad;
        }
    }

}

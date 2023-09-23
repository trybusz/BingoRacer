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
    public bool restarted;

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
            timeText.SetText(LevelAssets.ConvertTimeToString(runTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Start") && restarted) {
            inStart = true;

        }
        

    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Start")) {
            if (restarted) {
                timeOfStart = Time.timeSinceLevelLoad;
            }
            restarted = false;
            inStart = false;
            
        }
    }

}

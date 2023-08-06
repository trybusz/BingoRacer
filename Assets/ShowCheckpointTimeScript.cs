using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCheckpointTimeScript : MonoBehaviour
{
    private TMP_Text checkpointText = null;
    TimeScript timeScript = null;
    private int tempTimeMinutes;
    private float tempTimeSeconds;

    // Start is called before the first frame update
    void Start()
    {
        checkpointText = GameObject.Find("CheckpointTime").GetComponent<TMP_Text>();
        checkpointText.enabled = false;
        timeScript = gameObject.GetComponent<TimeScript>();
        tempTimeMinutes = 0;
        tempTimeSeconds = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Checkpoint") && other.GetComponent<CheckpointScript>().collected == false) {
            other.GetComponent<CheckpointScript>().collected = true;
            StopCoroutine(invisTime());
            checkpointText.enabled = true;
            tempTimeMinutes = timeScript.minutes;
            tempTimeSeconds = timeScript.seconds;
            if (tempTimeSeconds < 10 && tempTimeMinutes < 10) {
                checkpointText.SetText("0" + tempTimeMinutes + ":0" + tempTimeSeconds);
            }
            else if (tempTimeSeconds < 10) {
                checkpointText.SetText(tempTimeMinutes + ":0" + tempTimeSeconds);
            }
            else if (tempTimeMinutes < 10) {
                checkpointText.SetText("0" + tempTimeMinutes + ":" + tempTimeSeconds);
            }
            else {
                checkpointText.SetText(tempTimeMinutes + ":" + tempTimeSeconds);
            }
            StartCoroutine(invisTime());
        }
    }

    IEnumerator invisTime() {
        yield return new WaitForSeconds(2.5f);
        checkpointText.enabled = false;
    }
}

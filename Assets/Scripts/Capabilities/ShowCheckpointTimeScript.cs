using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShowCheckpointTimeScript : MonoBehaviour
{
    private TMP_Text checkpointText = null;
    private TMP_Text checkpointTimeDiffText = null;
    private TMP_Text checkpointCountText = null;
    TimeScript timeScript = null;
    public int checkpointCounter;
    public GameObject[] checkpoints;
    public List<float> bestCheckpointTimes;
    public List<float> checkpointTimes;

    // Start is called before the first frame update
    void Start() {
        // get previous best checkpoints from persistent data
        string levelSceneName = SceneManager.GetActiveScene().name;
        string levelFolderSceneName = LevelAssets.GetLevelFolderSceneName(levelSceneName);
        LevelTimesData levelTime = new();
        bestCheckpointTimes = levelTime.GetLevelCheckpointTimes(levelFolderSceneName, levelSceneName);
        // initialize checkpoint text objects
        checkpointText = GameObject.Find("CheckpointTime").GetComponent<TMP_Text>();
        checkpointText.enabled = false;
        checkpointTimeDiffText = GameObject.Find("CheckpointTimeDiff").GetComponent<TMP_Text>();
        checkpointTimeDiffText.enabled = false;
        checkpointCountText = GameObject.Find("CheckpointCounter").GetComponent<TMP_Text>();
        checkpointCounter = 0;
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        DisplayCheckpointCount();

        timeScript = gameObject.GetComponent<TimeScript>();
        checkpointTimes = new List<float>();
    }

    // Update is called once per frame
    void Update() {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Checkpoint") && other.GetComponent<CheckpointScript>().collected == false) {
            float currRunTime = timeScript.GetRunTime();
            float checkpointTimeDiff = 0f;
            if (bestCheckpointTimes != null && bestCheckpointTimes.Count > checkpointCounter) {
                checkpointTimeDiff = currRunTime - bestCheckpointTimes[checkpointCounter];
            }
            // count and track new collected checkpoint
            checkpointCounter++;
            DisplayCheckpointCount();
            other.GetComponent<CheckpointScript>().collected = true;
            checkpointTimes.Add(currRunTime);
            // display checkpoint time
            StopCoroutine(invisTime());
            checkpointText.enabled = true;
            checkpointText.SetText(LevelAssets.ConvertTimeToString(currRunTime));
            checkpointTimeDiffText.enabled = true;
            if ((int)(checkpointTimeDiff * 1000) < 0) {
                checkpointTimeDiffText.SetText("" + Mathf.Floor(checkpointTimeDiff * 1000) / 1000);
                checkpointTimeDiffText.color = new Color(0, 1, 0, 1);
            } else if ((int)(checkpointTimeDiff * 1000) > 0) {
                checkpointTimeDiffText.SetText("+" + Mathf.Floor(checkpointTimeDiff * 1000) / 1000);
                checkpointTimeDiffText.color = new Color(1, 0, 0, 1);
            } else {
                checkpointTimeDiffText.SetText("0");
                checkpointTimeDiffText.color = new Color(0, 1, 0, 1);
            }
            StartCoroutine(invisTime());
        }
    }

    IEnumerator invisTime() {
        yield return new WaitForSeconds(2.5f);
        checkpointText.enabled = false;
        checkpointTimeDiffText.enabled = false;
    }

    public void DisplayCheckpointCount() {
        checkpointCountText.SetText(checkpointCounter + "/" + checkpoints.Length);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Start")) {
            gameObject.GetComponent<ShowCheckpointTimeScript>().checkpointTimes.Clear();
        }
    }

}

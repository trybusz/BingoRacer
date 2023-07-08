using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTextManager : MonoBehaviour
{
    private TMP_Text moveText = null;
    private TMP_Text jumpText = null;
    private TMP_Text dashText = null;
    private TMP_Text restartText = null;
    private TMP_Text checkpointText = null;

    // Start is called before the first frame update
    void Start()
    {
        string rebinds = PlayerPrefs.GetString("rebinds");
        Debug.Log(rebinds);

        moveText = GameObject.Find("MoveText").GetComponent<TMP_Text>();
        jumpText = GameObject.Find("JumpText").GetComponent<TMP_Text>();
        dashText = GameObject.Find("DashText").GetComponent<TMP_Text>();
        restartText = GameObject.Find("RestartText").GetComponent<TMP_Text>();
        checkpointText = GameObject.Find("CheckpointText").GetComponent<TMP_Text>();

        moveText.SetText("Press " + "Phillip Fix" + " and " + "Phillip Fix" + " to move");
        jumpText.SetText("Press " + "Phillip Fix" + " to jump");
        dashText.SetText("Press " + "Phillip Fix" + " to dash");
        restartText.SetText("Press " + "Phillip Fix" + " to restart level");
        checkpointText.SetText("Press " + "Phillip Fix" +  " to go to the last checkpoint");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

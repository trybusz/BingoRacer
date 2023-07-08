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

    private static string[,] defaultControls = {
        {"MoveLeft"  , "a"},
        {"MoveRight" , "d"},
        {"Jump"      , "space"},
        {"Dash"      , "s"},
        {"Checkpoint", "t"},
        {"Restart"   , "r"}};

    // Start is called before the first frame update
    void Start()
    {
        string rebinds = PlayerPrefs.GetString("rebinds");
        Dictionary<string, string> controls = GetInputControls(rebinds);
        Debug.Log(rebinds);

        moveText = GameObject.Find("MoveText").GetComponent<TMP_Text>();
        jumpText = GameObject.Find("JumpText").GetComponent<TMP_Text>();
        dashText = GameObject.Find("DashText").GetComponent<TMP_Text>();
        restartText = GameObject.Find("RestartText").GetComponent<TMP_Text>();
        checkpointText = GameObject.Find("CheckpointText").GetComponent<TMP_Text>();

        moveText.SetText("Press [" + controls["MoveLeft"] + "] and [" + controls["MoveRight"] + "] to move");
        jumpText.SetText("Press [" + controls["Jump"] + "] to jump");
        dashText.SetText("Press [" + controls["Dash"] + "] to dash");
        restartText.SetText("Press [" + controls["Restart"] + "] to restart level");
        checkpointText.SetText("Press [" + controls["Checkpoint"] +  "] to go to the last checkpoint");
    }

    private Dictionary<string, string> GetInputControls(string rebinds) {
        Dictionary<string, string> controls = new Dictionary<string, string>();
        // parse string of modified controls
        if (rebinds != "") {
            int index = 13; // skip over {"bindings":[
            while (rebinds[index] == '{') {
                string action = "undefined";
                string key = "undefined";
                while (rebinds[index++] != '}') {
                    index++; // skip over "
                    string field = "";
                    while (rebinds[index] != '"') {
                        field += rebinds[index++];
                    }
                    index += 3; // skip over ":"
                    string value = "";
                    while (rebinds[index] != '"') {
                        value += rebinds[index++];
                    }
                    index++; // skip over "
                    if (field == "action") {
                        action = getLastWord(value);
                    }
                    else if (field == "path") {
                        key = getLastWord(value);
                    }
                }
                if (action == "Move") {
                    action += (controls.ContainsKey("MoveLeft") ? "Right" : "Left");
                }
                controls.Add(action, key);
                index++; // skip over ,
            }
        }
        // set unmodified controls
        for (int i = 0; i < defaultControls.GetLength(0); i++) {
            if (!controls.ContainsKey(defaultControls[i, 0])) {
                controls.Add(defaultControls[i, 0], defaultControls[i, 1]);
            }
        }
        return controls;
    }

    private string getLastWord(string input) {
        if (input == "" || input[input.Length - 1] == '/') {
            return "undefined";
        }
        int index = input.Length - 1;
        while (index - 1 >= 0 && input[index - 1] != '/') {
            index--;
        }
        return input.Substring(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

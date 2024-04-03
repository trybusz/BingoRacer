using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BingoBoardManager : NetworkBehaviour {
    private NetworkVariable<BingoBoard> board = new NetworkVariable<BingoBoard>();
    private NetworkVariable<byte> winner = new NetworkVariable<byte>(BingoBoard.NONE);
    private byte[] boardTeams;

    private MultiplayerManagerScript multiplayerScript;
    private TMP_InputField debugText;

    private Color BLUE = new(98f/255f, 161f/255f, 221f/255f);
    private Color RED = new(222f/255f, 97f/255f, 100f/255f);

    void Start() {
        multiplayerScript = GameObject.FindGameObjectWithTag("MultiplayerManager").GetComponent<MultiplayerManagerScript>();
        GameObject dto = GameObject.Find("DebugText");
        debugText = dto.GetComponent<TMP_InputField>();
        if (multiplayerScript.IsHost()) {
            board.Value = new BingoBoard();
        }
        boardTeams = new byte[25];
        for (int i = 0; i < boardTeams.Length; i++) {
            boardTeams[i] = BingoBoard.NONE;
        }
    }

    void Update() {
        for (int i = 0; i < 25; i++) {
            byte serverTeam = board.Value.GetTeam(i);
            if (serverTeam != boardTeams[i]) {
                SetSquareColor(i, serverTeam);
                boardTeams[i] = serverTeam;
            }
        }
        if (winner.Value == BingoBoard.TEAM1) {
            debugText.text = "Red Wins";
        } else if (winner.Value == BingoBoard.TEAM2) {
            debugText.text = "Blue Wins";
        }
    }

    public void ShowBingoLevelData(int index) {
        float bestTime = board.Value.GetBestTime(index);
        string bestTimeStr = "Unclaimed";
        if (bestTime != 0f) {
            bestTimeStr = LevelAssets.ConvertTimeToString(bestTime);
        }

        // display bestTimeStr
        Debug.Log(bestTimeStr);
    }

    public void PlayBingoLevel(int index) {
        string levelSceneName = board.Value.GetLevelName(index);
        SceneContext.SetElement("Level", levelSceneName);
        SceneContext.SetElement("BingoIndex", index.ToString());
        // SubmitLevelTime(index, 1f);
        SceneManager.LoadScene(levelSceneName, LoadSceneMode.Additive);
    }

    private void SetSquareColor(int index, byte team) {
        GameObject square = gameObject.transform.parent.GetChild(index + 3).gameObject;
        if (team == BingoBoard.TEAM1) {
            square.GetComponent<Image>().color = RED;
        } else if (team == BingoBoard.TEAM2) {
            square.GetComponent<Image>().color = BLUE;
        }
    }

    public void SubmitLevelTime(int index, float time) {
        byte team = multiplayerScript.GetTeam() == MultiplayerManagerScript.TEAM_RED ? BingoBoard.TEAM1 : BingoBoard.TEAM2;
        if (time < board.Value.GetBestTime(index)) {
            SubmitLevelTimeServerRpc(index, team, time);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SubmitLevelTimeServerRpc(int index, byte team, float time) {
        byte winningTeam = board.Value.SubmitTime(index, team, time);
        board.SetDirty(true);
        if (winningTeam != BingoBoard.NONE) {
            winner.Value = winningTeam;
        }
    }
}

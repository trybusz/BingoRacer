using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingoBoard : object
{

    public const int NONE = 1;
    public const int TEAM1 = 2;
    public const int TEAM2 = 4;

    private struct Space {
        public string levelName;
        public float bestTime;
        public int recordHolder;
        public Space(string name, float time, int holder) {
            levelName = name;
            bestTime = time;
            recordHolder = holder;
        }
    }

    private Space[] board;

    public BingoBoard() {
        string[] levelSceneNames = LevelAssets.GetBingoLevelSceneNames();
        randomizeStringArray(levelSceneNames);
        board = new Space[25];
        for (int i = 0; i < 25; i++) {
            board[i] = new Space(levelSceneNames[i], 0f, NONE);
        }
    }

    public float GetBestTime(int index) {
        return board[index].bestTime;
    }

    public int SubmitTime(int index, int team, float time) {
        if (time < board[index].bestTime) {
            board[index].bestTime = time;
            return isBingo();
        }
        return NONE;
    }

    private void randomizeStringArray(string[] values) {
        int i = values.Length;
        while (i > 1) {
            int index = Random.Range(0, i--);
            string temp = values[i];
            values[i] = values[index];
            values[index] = temp;
        }
    }

    private int isBingo() {
        // rows and columns
        int flag;
        for (int i = 0; i < 5; i++) {
            // check vertically
            flag = board[i].recordHolder;
            flag |= board[i+5].recordHolder;
            flag |= board[i+10].recordHolder;
            flag |= board[i+15].recordHolder;
            flag |= board[i+20].recordHolder;
            if (flag == TEAM1 || flag == TEAM2) return flag;
            // check horizontally
            flag = board[i*5].recordHolder;
            flag |= board[i*5+1].recordHolder;
            flag |= board[i*5+2].recordHolder;
            flag |= board[i*5+3].recordHolder;
            flag |= board[i*5+4].recordHolder;
            if (flag == TEAM1 || flag == TEAM2) return flag;
        }
        // check diagonals
        flag = board[0].recordHolder;
        flag |= board[6].recordHolder;
        flag |= board[12].recordHolder;
        flag |= board[18].recordHolder;
        flag |= board[24].recordHolder;
        if (flag == TEAM1 || flag == TEAM2) return flag;
        flag = board[4].recordHolder;
        flag |= board[8].recordHolder;
        flag |= board[12].recordHolder;
        flag |= board[16].recordHolder;
        flag |= board[20].recordHolder;
        if (flag == TEAM1 || flag == TEAM2) return flag;
        return NONE;
    }
}

using Unity.Netcode;
using UnityEngine;

public class BingoBoard : INetworkSerializable {

    public const byte NONE = 1;
    public const byte TEAM1 = 2;
    public const byte TEAM2 = 4;

    private struct Space : INetworkSerializable {
        public string levelName;
        public float bestTime;
        public byte recordHolder;
        public Space(string name, float time, byte holder) {
            levelName = name;
            bestTime = time;
            recordHolder = holder;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
            if (serializer.IsReader) {
                ushort levelKey = 0;
                serializer.SerializeValue(ref levelKey);
                levelName = LevelAssets.GetLevelSceneNameFromKey(levelKey);
            } else {
                ushort levelKey = LevelAssets.GetLevelKeyFromSceneName(levelName);
                serializer.SerializeValue(ref levelKey);
            }
            serializer.SerializeValue(ref bestTime);
            serializer.SerializeValue(ref recordHolder);
        }
    }

    private Space[] board;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
        if (board == null) {
            board = new Space[25];
        }
        for (int i = 0; i < board.Length; i++) {
            board[i].NetworkSerialize(serializer);
        }
    }

    public BingoBoard() {
        string[] levelSceneNames = LevelAssets.GetBingoLevelSceneNames();
        randomizeStringArray(levelSceneNames);
        board = new Space[25];
        for (int i = 0; i < 25; i++) {
            board[i] = new Space(levelSceneNames[i], 100000000f, NONE);
        }
    }

    public string GetLevelName(int index) {
        return board[index].levelName;
    }

    public float GetBestTime(int index) {
        return board[index].bestTime;
    }

    public byte GetTeam(int index) {
        return board[index].recordHolder;
    }

    public byte SubmitTime(int index, byte team, float time) {
        if (time < board[index].bestTime) {
            board[index].bestTime = time;
            board[index].recordHolder = team;
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

    private byte isBingo() {
        // rows and columns
        byte flag;
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

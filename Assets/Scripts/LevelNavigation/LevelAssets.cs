using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelAssets : object
{

    public struct LevelInfo {
        public string displayName;
        public string sceneName;
        public float makerTime;
        public LevelInfo(string displayName, string sceneName, float makerTime) {
            this.displayName = displayName;
            this.sceneName = sceneName;
            this.makerTime = makerTime;
        }
    }

    public struct LevelLocation {
        public string displayName;
        public string sceneName;
        public LevelInfo[] levels;
        public LevelLocation(string displayName, string sceneName, LevelInfo[] levels) {
            this.displayName = displayName;
            this.sceneName = sceneName;
            this.levels = levels;
        }
    }

    private static bool isInit = false;
    public static readonly LevelLocation[] levelDirectories = {
        new LevelLocation(
            "OGLevels",
            "OG_Lvl_Select",
            new LevelInfo[25]{
                new LevelInfo("Level 1", "OG_Lvl_1", 26.999f),
                new LevelInfo("Level 2", "OG_Lvl_2", 29.956f),
                new LevelInfo("Level 3", "OG_Lvl_3", 671.111f),
                new LevelInfo("Level 4", "OG_Lvl_4", 671.111f),
                new LevelInfo("Level 5", "OG_Lvl_5", 671.111f),
                new LevelInfo("Level 6", "OG_Lvl_6", 27.316f),
                new LevelInfo("Level 7", "OG_Lvl_7", 671.111f),
                new LevelInfo("Level 8", "OG_Lvl_8", 671.111f),
                new LevelInfo("Level 9", "OG_Lvl_9", 29.320f),
                new LevelInfo("Level 10", "OG_Lvl_10", 30.419f),
                new LevelInfo("Level 11", "OG_Lvl_11", 671.111f),
                new LevelInfo("Level 12", "OG_Lvl_12", 26.500f),
                new LevelInfo("Level 13", "OG_Lvl_13", 671.111f),
                new LevelInfo("Level 14", "OG_Lvl_14", 671.111f),
                new LevelInfo("Level 15", "OG_Lvl_15", 25.360f),
                new LevelInfo("Level 16", "OG_Lvl_16", 671.111f),
                new LevelInfo("Level 17", "OG_Lvl_17", 671.111f),
                new LevelInfo("Level 18", "OG_Lvl_18", 29.215f),
                new LevelInfo("Level 19", "OG_Lvl_19", 31.599f),
                new LevelInfo("Level 20", "OG_Lvl_20", 671.111f),
                new LevelInfo("Level 21", "OG_Lvl_21", 671.111f),
                new LevelInfo("Level 22", "OG_Lvl_22", 21.379f),
                new LevelInfo("Level 23", "OG_Lvl_23", 671.111f),
                new LevelInfo("Level 24", "OG_Lvl_24", 37.079f),
                new LevelInfo("Level 25", "OG_Lvl_25", 671.111f),
            }
        )
    };

    public static Dictionary<string, int> levelDirectoryIndices = new Dictionary<string, int>();
    public static Dictionary<string, int> levelIndices = new Dictionary<string, int>();

    public static void InitLevelDirectories() {
        if (isInit) return;
        isInit = true;
        for (int i = 0; i < levelDirectories.Length; i++) {
            for (int j = 0; j < levelDirectories[i].levels.Length; j++) {
                levelDirectoryIndices.Add(levelDirectories[i].levels[j].sceneName, i);
                levelIndices.Add(levelDirectories[i].levels[j].sceneName, j);
            }
        }
    }

    public static string GetLevelFolderSceneName(string currLevelSceneName) {
        int directoryIndex = levelDirectoryIndices[currLevelSceneName];
        return levelDirectories[directoryIndex].sceneName;
    }

    public static string GetLevelFolderDisplayName(string currLevelSceneName) {
        int directoryIndex = levelDirectoryIndices[currLevelSceneName];
        return levelDirectories[directoryIndex].displayName;
    }

    public static string GetNextLevelSceneName(string currLevelSceneName) {
        int directoryIndex = levelDirectoryIndices[currLevelSceneName];
        int levelIndex = levelIndices[currLevelSceneName];
        if (levelIndex < levelDirectories[directoryIndex].levels.Length) {
            return levelDirectories[directoryIndex].levels[levelIndex + 1].sceneName;
        }
        return null;
    }

    public static string GetLevelDisplayName(string levelSceneName) {
        int directoryIndex = levelDirectoryIndices[levelSceneName];
        int levelIndex = levelIndices[levelSceneName];
        return levelDirectories[directoryIndex].levels[levelIndex].displayName;
    }

    public static float GetLevelMakerTime(string levelSceneName) {
        int directoryIndex = levelDirectoryIndices[levelSceneName];
        int levelIndex = levelIndices[levelSceneName];
        return levelDirectories[directoryIndex].levels[levelIndex].makerTime;
    }

    public static string ConvertTimeToString(float time) {
        time = Mathf.Abs(time);
        int min = (int)time / 60;
        int sec = (int)time % 60;
        int mil = (int)Mathf.Round((time % 1) * 1000);
        string minStr = min.ToString();
        string secStr = sec.ToString();
        string milStr = mil.ToString();
        if (min < 10) {
            minStr = "0" + minStr;
        }
        if (sec < 10) {
            secStr = "0" + secStr;
        }
        if (mil < 10) {
            milStr = "00" + milStr;
        }
        else if (mil < 100) {
            milStr = "0" + milStr;
        }
        return minStr + ":" + secStr + "." + milStr;
    }

    // private static GameAssets _i;
    // public static GameAssets i
    // {
    //     get
    //     {
    //         if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
    //         return _i;
    //     }
    // }
    // public GameObject enemySnowball;
    // public const int NONE = -1;
    // public static readonly int[] UTILITY_MANA_COSTS = { 5, 1, 1, 1, 1, 1, 40, 1 };
}

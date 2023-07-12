using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelAssets : object
{
    public struct LevelLocation {
        public string name;
        public string sceneName;
        public string []levelNames;
        public LevelLocation(string name, string sceneName, string[] levelNames) {
            this.name = name;
            this.sceneName = sceneName;
            this.levelNames = levelNames;
        }
    }

    private static bool isInit = false;
    // level names must be unique (temp bad solution) or the game needs to keep track of the current level folder
    public static readonly LevelLocation[] levelDirectories = {
        new LevelLocation("OGLevels",
                          "OG_Lvl_Select",
                          new string[25]{"OG_Lvl_1", "OG_Lvl_2", "OG_Lvl_3", "OG_Lvl_4", "OG_Lvl_5",
                                         "OG_Lvl_6", "OG_Lvl_7", "OG_Lvl_8", "OG_Lvl_9", "OG_Lvl_10",
                                         "OG_Lvl_11", "OG_Lvl_12", "OG_Lvl_13", "OG_Lvl_14", "OG_Lvl_15",
                                         "OG_Lvl_16", "OG_Lvl_17", "OG_Lvl_18", "OG_Lvl_19", "OG_Lvl_20",
                                         "OG_Lvl_21", "OG_Lvl_22", "OG_Lvl_23", "OG_Lvl_24", "OG_Lvl_25"})
        };

    public static Dictionary<string, int> levelDirectoryIndices = new Dictionary<string, int>();
    public static Dictionary<string, int> levelIndices = new Dictionary<string, int>();

    public static void InitLevelDirectories() {
        if (isInit) return;
        isInit = true;
        for (int i = 0; i < levelDirectories.Length; i++) {
            for (int j = 0; j < levelDirectories[i].levelNames.Length; j++) {
                levelDirectoryIndices.Add(levelDirectories[i].levelNames[j], i);
                levelIndices.Add(levelDirectories[i].levelNames[j], j);
            }
        }
    }

    public static string GetLevelSelectSceneName(string currLevel) {
        int directoryIndex = levelDirectoryIndices[currLevel];
        return levelDirectories[directoryIndex].sceneName;
    }

    public static string GetNextLevelName(string currLevel) {
        int directoryIndex = levelDirectoryIndices[currLevel];
        int levelIndex = levelIndices[currLevel];
        if (levelIndex < levelDirectories[directoryIndex].levelNames.Length) {
            return levelDirectories[directoryIndex].levelNames[levelIndex + 1];
        }
        return null;
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

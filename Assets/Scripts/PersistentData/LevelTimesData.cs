using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTimesData {

    public List<LevelFolder> levelFolders;
    
    public LevelTimesData() {
        this.GetLevelTimeData();
    }

    public int UpdateLevelTime(string folderName, string levelName, float time) {
        for (int i = 0; i < levelFolders.Count; i++) {
            if (levelFolders[i].name == folderName) {
                for (int j = 0; j < levelFolders[i].levelTimes.Count; j++) {
                    if (levelFolders[i].levelTimes[j].name == levelName) {
                        levelFolders[i].levelTimes[j].time = time;
                        PersistentDataSaveLoad.WriteToFile("LevelTimesData", this.ToJson());
                        return 0;
                    }
                }
                return -1; // level not found in folder
            }
        }
        return -2; // folder not found
    }

    public float GetLevelTime(string folderName, string levelName) {
        foreach(LevelFolder folder in levelFolders) {
            if (folder.name == folderName) {
                foreach(LevelTime level in folder.levelTimes) {
                    if (level.name == levelName) {
                        return level.time;
                    }
                }
            }
        }
        return -1f;
    }

    [System.Serializable]
    public class LevelTime {
        public string name;
        public float time;
        public LevelTime(string levelName) {
            name = levelName;
            time = 3355.555f;
        }
    }

    [System.Serializable]
    public class LevelFolder {
        public string name;
        public List<LevelTime> levelTimes;
        public LevelFolder(string folderName, List<LevelTime> levelTimes) {
            name = folderName;
            this.levelTimes = levelTimes;
        }
    }

    private void GetLevelTimeData() {
        if (PersistentDataSaveLoad.FileExists("LevelTimesData")) {
            string levelTimeDataJson = "";
            PersistentDataSaveLoad.LoadFromFile("LevelTimesData", out levelTimeDataJson); 
            LoadFromJson(levelTimeDataJson);
        }
        else {
            this.InitLevelData();
        }
    }

    private string ToJson() {
        return JsonUtility.ToJson(this);
    }

    private void LoadFromJson(string a_Json) {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }

    private void InitLevelData() {
        // Zach is the worst for making me hard code this :)
        
        List<LevelTime> Folder1_levelTimes = new List<LevelTime>();
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_1"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_2"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_3"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_4"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_5"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_6"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_7"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_8"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_9"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_10"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_11"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_12"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_13"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_14"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_15"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_16"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_17"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_18"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_19"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_20"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_21"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_22"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_23"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_24"));
        Folder1_levelTimes.Add(new LevelTime("OG_Lvl_25"));

        List<LevelFolder> levelFolders = new List<LevelFolder>();
        levelFolders.Add(new LevelFolder("OGLevels", Folder1_levelTimes));

        this.levelFolders = levelFolders;
    }
}
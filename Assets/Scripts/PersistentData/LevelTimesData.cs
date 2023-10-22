using System.Collections.Generic;
using UnityEngine;

// TODO: probably only write one folder of data per file. otherwise,
// this will bloat really badly as we add lots of levels
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

    public int UpdateLevelCheckpointTimes(string folderName, string levelName, List<float> checkpointTimes) {
        for (int i = 0; i < levelFolders.Count; i++) {
            if (levelFolders[i].name == folderName) {
                for (int j = 0; j < levelFolders[i].levelTimes.Count; j++) {
                    if (levelFolders[i].levelTimes[j].name == levelName) {
                        levelFolders[i].levelTimes[j].checkpointTimes = checkpointTimes;
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

    public List<float> GetLevelCheckpointTimes(string folderName, string levelName) {
        foreach(LevelFolder folder in levelFolders) {
            if (folder.name == folderName) {
                foreach(LevelTime level in folder.levelTimes) {
                    if (level.name == levelName) {
                        return level.checkpointTimes;
                    }
                }
            }
        }
        return null;
    }

    [System.Serializable]
    public class LevelTime {
        public string name;
        public float time;
        public List<float> checkpointTimes;
        public LevelTime(string levelName) {
            name = levelName;
            time = 3355.555f;
            checkpointTimes = null;
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
        
        List<LevelTime> Folder1_levelTimes = new();
        Folder1_levelTimes.Add(new LevelTime("OGLevel1"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel2"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel3"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel4"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel5"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel6"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel7"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel8"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel9"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel10"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel11"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel12"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel13"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel14"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel15"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel16"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel17"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel18"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel19"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel20"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel21"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel22"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel23"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel24"));
        Folder1_levelTimes.Add(new LevelTime("OGLevel25"));

        List<LevelFolder> levelFolders = new();
        levelFolders.Add(new LevelFolder("OGLevelSelect", Folder1_levelTimes));

        this.levelFolders = levelFolders;
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class PersistentDataSaveLoad {
    
    static string persistentDataPath;

    static PersistentDataSaveLoad() {
        // TODO: change this to a static function which is called in the
        // "Awake" or "Start" function of some other class which will only
        // be called once and will be called prior to the need to load or
        // save game state.
        persistentDataPath = Application.persistentDataPath;
    }

    public static bool WriteToFile(string fileName, string fileData)
    {
        var fullPath = Path.Combine(persistentDataPath, fileName);

        try
        {
            File.WriteAllText(fullPath, fileData);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }

    public static bool LoadFromFile(string fileName, out string fileData)
    {
        var fullPath = Path.Combine(persistentDataPath, fileName);

        try
        {
            fileData = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            fileData = "";
            return false;
        }
    }

    public static bool FileExists(string fileName) {
        var fullPath = Path.Combine(persistentDataPath, fileName);
        return File.Exists(fullPath);
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class PersistentDataSaveLoad
{
    public static bool WriteToFile(string fileName, string fileData)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);

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
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);

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
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);
        return File.Exists(fullPath);
    }
}
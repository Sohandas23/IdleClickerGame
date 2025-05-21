using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath;

    private static string GetSavePath()
    {
        if (string.IsNullOrEmpty(savePath))
        {
            savePath = Path.Combine(Application.persistentDataPath, "save.json");
        }

        return savePath;
    }

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSavePath(), json);
        Debug.Log("Game Saved to: " + GetSavePath());
    }

    public static SaveData Load()
    {
        if (File.Exists(GetSavePath()))
        {
            string json = File.ReadAllText(GetSavePath());
            return JsonUtility.FromJson<SaveData>(json);
        }

        return null;
    }

    public static void DeleteSave()
    {
        if (File.Exists(GetSavePath()))
            File.Delete(GetSavePath());
    }
}

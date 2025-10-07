using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static string CurrentSlot { get; private set;} = "slot1";

    static string Dir => Path.Combine(Application.persistentDataPath);
    static string PathFor(string slot) => System.IO.Path.Combine(Dir, $"{slot}.json");

    public static void SetSlot (string slotName)
    {
        if (string.IsNullOrWhiteSpace(slotName)) return;

        CurrentSlot = slotName;
    }

    public static void Save(SaveData data)
    {
        try
        {
            if (!Directory.Exists(Dir)) Directory.CreateDirectory(Dir);
            data.savedAtIsoUtc = DateTime.UtcNow.ToString("0");
            var json = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(PathFor(CurrentSlot), json);

            Debug.Log ($"[SaveSystem] Guardado en: {PathFor(CurrentSlot)}");

        }
        catch (Exception e)
        {
            Debug.LogError ($"[SaveSystem] Error al guardar: {e}");
        }
    }

    public static bool tryLoad (out SaveData data)
    {
        var path = PathFor(CurrentSlot);
        if (!File.Exists(path))
        {
            data = null;
            return false;
        }

        try
        {
            var json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveSystem] Error al cargar: {e}");
            data = null;
        }

        return data != null;
    }

    public static void Delete()
    {
        var path = PathFor(CurrentSlot);
        if (File.Exists(path)) File.Delete(path);
    }

    public static string GetSavePath() => PathFor(CurrentSlot);
}

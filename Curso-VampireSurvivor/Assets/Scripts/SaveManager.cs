using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    // Singleton
    public static SaveManager Instance { get; private set; }

    [Header("Autosave")]
    public bool enableAutosave = true;
    public float autosaveEverySeconds = 30f;

    public Transform player;
    float playTime;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SaveSystem.tryLoad(out var data))
        {
            Apply (data);
        }

        if (enableAutosave) StartCoroutine(AutosaveLoop());
    }

    // Update is called once per frame
    void Update()
    {
        playTime += Time.deltaTime;   
    }

    IEnumerator AutosaveLoop()
    {
        var wait = new WaitForSeconds(autosaveEverySeconds);
        while (true)
        {
            yield return wait;
            SaveSystem.SetSlot("slot1");
            ManualSave();
        }
    }

    public void ManualSave()
    {
        var data = BuildSave();
        SaveSystem.Save(data);
    }

    public void ManualLoad()
    {
        if (SaveSystem.tryLoad(out var data))
        {
            Apply (data);
        }
    }

    public void WipeSave()
    {
        SaveSystem.Delete();
    }

    SaveData BuildSave()
    {
        SaveData d = new SaveData();

        d.version = 1;
        d.lastScene = SceneManager.GetActiveScene().name;
        d.playtimeSeconds = playTime;

        if (player != null) d.playerPos = player.position;

        return d;
    }

    void Apply (SaveData d)
    {
        playTime = d.playtimeSeconds;

        if (player != null)
        {
            player.position = d.playerPos;
        }

        if (SceneManager.GetActiveScene().name != d.lastScene)
        {
            SceneManager.LoadScene(d.lastScene);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveSystem.SetSlot("pause");
            ManualSave();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SetSlot("quit");
        ManualSave();
    }
}


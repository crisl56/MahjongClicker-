using System;
using System.IO;
using BreakInfinity;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"More than one SaveManager exist in the scene! Destroying {this.gameObject.name}");
            Destroy(this.gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        // everytime the application starts just try to load.
        Load();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        SaveData data = new SaveData
        {
            coins = CoinManager.Instance.Coins.ToString(),
            lastSaveTime = DateTime.UtcNow.Ticks,
            upgrades = UpgradeManager.Instance.GetSaveData()
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public void Load()
    {
        if (!File.Exists(SavePath)) return;

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        CoinManager.Instance.SetCoins(BigDouble.Parse(data.coins));
        UpgradeManager.Instance.LoadSaveData(data.upgrades);
    }

    public void ClearSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Save cleared.");
        }
    }

    private void ApplyOfflineEarnings(long lastSaveTicks)
    {
        DateTime lastSave = new DateTime(lastSaveTicks, DateTimeKind.Utc);
        double secondsOffline = (DateTime.UtcNow - lastSave).TotalSeconds;

        // 24 hour cap
        secondsOffline = Math.Min(secondsOffline, 86400);

        if (secondsOffline <= 0) return;

        BigDouble earned = CoinManager.Instance.CoinsPerSecond * secondsOffline;
        CoinManager.Instance.AddCoins(earned);

        Debug.Log($"Offline for {secondsOffline:F0}s, earned {earned}");
    }

    [ContextMenu("DEBUG - Save")]
    public void DebugSave() => Save();

    [ContextMenu("DEBUG - Load")]
    public void DebugLoad() => Load();

    [ContextMenu("DEBUG - Clear Save")]
    public void DebugClearSave() => ClearSave();

    [ContextMenu("DEBUG - Simulate 1 Hour Offline")]
    public void DebugSimulateOffline()
    {
        ApplyOfflineEarnings(DateTime.UtcNow.AddHours(-1).Ticks);
    }

    [ContextMenu("DEBUG - Print Save Path")]
    public void DebugPrintSavePath() => Debug.Log(SavePath);
}


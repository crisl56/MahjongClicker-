using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public string coins;
    public long lastSaveTime;
    public List<UpgradeSaveData> upgrades = new();
}
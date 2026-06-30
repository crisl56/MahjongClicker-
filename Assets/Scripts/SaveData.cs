using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public string coins;
    public long lastSaveTime;
    public StatHolderSaveData statHolder = new();
}
[System.Serializable]
public class StatHolderSaveData
{
    public List<string> ActiveBuffNames = new();
}
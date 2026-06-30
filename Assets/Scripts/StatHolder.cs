using BreakInfinity;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class StatHolder
{
    public readonly Dictionary<StatType, Stat> Stats;

    public readonly List<SourceBuffInstance> Buffs = new();

    public StatHolder()
    {
        Stats = Enum.GetValues(typeof(StatType)).Cast<StatType>().ToDictionary(
            stat => stat,
            stat => new Stat()
            );
    }
    public BigDouble Calculate(StatType statType, BigDouble baseStat)
    {
        if (Stats == null || !Stats.ContainsKey(statType)) return baseStat;
        return Stats[statType].Evaluate(baseStat);
    }

    public void AddBuff(SourceBuffInstance buff)
    {
        Buffs.Add(buff);
        buff.Activate();
    }

    public bool HasBuff(BuffSO buffData)
    {   
        foreach(var buff in Buffs)
        {
            if (buff.Data.Name == buffData.Name)
            {
                return true;
            }
        }

        return false;
    }

    public void CreateBuff(BuffSO buffData)
    {
        if (buffData == null) return;

        SourceBuffInstance newBuff = new(buffData, this);
        AddBuff(newBuff);

    }
    public void RemoveBuff(SourceBuffInstance buff)
    {
        if (Buffs.Contains(buff))
            Buffs.Remove(buff);
    }

    public Stat GetStat(StatType statType)
    {
        if (Stats == null) return null;
        return Stats.TryGetValue(statType, out var stat) ? stat : null; // return stat or null
    }

    public StatHolderSaveData GetSaveData()
    {
        var data = new StatHolderSaveData();
        foreach (var buff in Buffs)
            data.ActiveBuffNames.Add(buff.Data.Name);
        return data;
    }

    public void LoadSaveData(StatHolderSaveData data, BuffDatabase database)
    {
        foreach (var name in data.ActiveBuffNames)
        {
            var buffSO = database.Find(name);
            if (buffSO != null)
                CreateBuff(buffSO);
        }
    }

}

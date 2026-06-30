public class SourceBuffInstance
{
    public BuffSO Data;
    private readonly StatHolder _holder;

    public SourceBuffInstance (BuffSO data, StatHolder holder)
    {
        Data = data;
        _holder = holder;
    }

    public void Activate()
    {

        foreach(var def in Data.Modifiers)
        {
            Stat stat = _holder.GetStat(def.TargetStat);
            if (stat != null)
            {
                StatModifier mod = new(def.Value, def.Type, this);
                stat.AddModifier(mod);
            }
        }
    }

    public void Deactivate()
    {
        foreach (var def in Data.Modifiers)
        {
            Stat stat = _holder.GetStat(def.TargetStat);

            if (stat != null)
            {
                stat.RemoveModifiersOfSource(this);
            }
        }

        _holder.RemoveBuff(this);
    }

}

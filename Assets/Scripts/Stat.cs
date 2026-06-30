using BreakInfinity;
using System;
using System.Collections.Generic;

public class Stat
{
    private List<StatModifier> _statModifiers = new();
    public event Action OnValueChanged;

    public BigDouble Evaluate(BigDouble baseStat)
    {
        BigDouble finalStat = baseStat;
        foreach (var mod in _statModifiers)
        {
            switch(mod.Type)
            {
                case StatModifierType.Flat:
                    finalStat += mod.Value;
                    break;
                case StatModifierType.Add:
                    finalStat += (baseStat * mod.Value);
                    break;
                case StatModifierType.Mult:
                    finalStat *= (1 + mod.Value);
                    break;
            }
        }

        return finalStat;
    }

    public void AddModifier(StatModifier modifier)
    {
        _statModifiers.Add(modifier);
        _statModifiers.Sort((a, b) => a.Type.CompareTo(b.Type));
        OnValueChanged?.Invoke();
    }

    public void RemoveModifiersOfSource(SourceBuffInstance source)
    {
        if (_statModifiers.RemoveAll(mod => mod.Source == source) > 0)
        {
            OnValueChanged?.Invoke();
        }
    }
}

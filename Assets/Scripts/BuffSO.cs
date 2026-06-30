using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats Modifer", menuName = "Game Data/Stats Modifer")]
public class BuffSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; } = "New Buff";

    [SerializeField] private List<ModifierDefinition> _modifiers = new();

    public IReadOnlyList<ModifierDefinition> Modifiers => _modifiers;

    [System.Serializable]
    public struct ModifierDefinition
    {
        public StatType TargetStat;
        public float Value;
        public StatModifierType Type;
    }
}
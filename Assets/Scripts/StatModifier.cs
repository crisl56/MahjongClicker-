public class StatModifier
{

    public readonly float Value;
    public readonly StatModifierType Type;
    public readonly SourceBuffInstance Source;

    public StatModifier(float value, StatModifierType type, SourceBuffInstance source)
    {
        Value = value;
        Type = type;
        Source = source;
    }
}

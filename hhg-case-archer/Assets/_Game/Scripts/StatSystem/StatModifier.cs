using _Game.Enums;

namespace _Game.StatSystem
{
    public class StatModifier
    {
        public float Value;
        public ModifierType Type;
        public bool IsTemporary;
        public float Duration;
    
        public StatModifier(float value, ModifierType type, bool isTemporary = false, float duration = 0)
        {
            Value = value;
            Type = type;
            IsTemporary = isTemporary;
            Duration = duration;
        }
    }
}
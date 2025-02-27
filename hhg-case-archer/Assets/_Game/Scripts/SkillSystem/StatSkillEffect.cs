using _Game.CombatSystem;
using _Game.DataStructures;
using _Game.Enums;
using _Game.StatSystem;
using UnityEngine;

namespace _Game.SkillSystem.StatSkills
{
    [CreateAssetMenu(fileName = "StatSkillEffect", menuName = "SkillSystem/Stat Skill Effect", order = 0)]
    public class StatSkillEffect : SkillEffectData
    {
        
        [SerializeField] private StatType statType;
        [SerializeField] private float value;
        [SerializeField] private ModifierType modifierType;
        [SerializeField] private float duration =0 ;
        public override void ApplyEffect(BaseCharacter character)
        {
            var modifier = new StatModifier(value, modifierType,duration);
            character.StatController.AddStatModifier(statType, modifier);
        }

        public override void RemoveEffect(BaseCharacter character)
        {
            var modifier = new StatModifier(value, modifierType,duration);
            character.StatController.RemoveStatModifier(statType, modifier);
        }
    }
}
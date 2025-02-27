using System.Collections.Generic;
using _Game.CombatSystem;
using _Game.Enums;
using UnityEngine;

namespace _Game.SkillSystem
{
    [CreateAssetMenu(fileName = "BaseSkill", menuName = "SkillSystem/BaseSkill", order = 0)]
    public class BaseSkill : ScriptableObject
    {
        [SerializeField] private string skillName;
        [SerializeField] private string description;
        [SerializeField] private SkillType skillType;
        [SerializeField] private float cooldown;
        [SerializeField] private float cost;
        [SerializeField] private Sprite icon;
        [SerializeField] private List<SkillEffectData> skillEffects;
        
        public string SkillName => skillName;
        public float Cooldown => cooldown;
        public float Cost => cost;
        public SkillType SkillType => skillType;

        public void ApplySkill(BaseCharacter character)
        {
            foreach (var effect in skillEffects)
            {
                effect.ApplyEffect(character);
            }
        }

        public void RemoveSkill(BaseCharacter character)
        {
            foreach (var effect in skillEffects)
            {
                effect.RemoveEffect(character);
            }
            
        }
    }
}
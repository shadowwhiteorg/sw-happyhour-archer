using _Game.CombatSystem;
using _Game.Enums;
using _Game.Interfaces;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Game.SkillSystem
{
    [CreateAssetMenu(fileName = "FireArrowBehavior", menuName = "CombatSystem/Projectile/ProjectileBehaviors/FireArrowBehavior", order = 0)]
    public class FireArrowBehavior : ProjectileBehavior
    {
        [SerializeField] private float burnDamage;
        public override void ApplyEffect(IDamageable target)
        {
            target.ApplyStatusEffect(StatusEffectType.Fire,0,burnDamage);
            // throw new System.NotImplementedException();
        }
    }
}
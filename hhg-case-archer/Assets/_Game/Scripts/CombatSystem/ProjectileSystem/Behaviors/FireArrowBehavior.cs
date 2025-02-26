using _Game.CombatSystem;
using _Game.Interfaces;
using UnityEngine;

namespace _Game.SkillSystem
{
    [CreateAssetMenu(fileName = "FireArrowBehavior", menuName = "CombatSystem/Projectile/ProjectileBehaviors/FireArrowBehavior", order = 0)]
    public class FireArrowBehavior : ProjectileBehavior
    {
        public override void ApplyEffect(IDamageable target)
        {
            // throw new System.NotImplementedException();
        }
    }
}
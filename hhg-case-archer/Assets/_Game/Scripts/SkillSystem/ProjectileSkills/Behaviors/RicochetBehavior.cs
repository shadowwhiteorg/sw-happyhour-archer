using _Game.CombatSystem;
using _Game.Interfaces;
using UnityEngine;

namespace _Game.SkillSystem
{
    [CreateAssetMenu(fileName = "RicochetBehavior", menuName = "CombatSystem/Projectile/ProjectileBehaviors/RicochetBehavior", order = 0)]
    public class RicochetBehavior : ProjectileBehavior
    {
        public override void ApplyEffect(Projectile projectile, IDamageable target)
        {
            throw new System.NotImplementedException();
        }
    }
}
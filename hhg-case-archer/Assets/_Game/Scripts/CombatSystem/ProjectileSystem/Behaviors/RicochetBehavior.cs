using _Game.Interfaces;
using UnityEngine;

namespace _Game.CombatSystem
{
    [CreateAssetMenu(fileName = "RicochetBehavior", menuName = "CombatSystem/Projectile/ProjectileBehaviors/RicochetBehavior", order = 0)]
    public class RicochetBehavior : ProjectileBehavior
    {
        public override void ApplyEffect(IDamageable target)
        {
            
        }
    }
}
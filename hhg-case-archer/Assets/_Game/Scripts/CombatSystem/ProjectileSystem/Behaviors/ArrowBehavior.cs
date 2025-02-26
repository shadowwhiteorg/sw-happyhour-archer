using _Game.Interfaces;
using UnityEngine;

namespace _Game.CombatSystem
{
    [CreateAssetMenu(fileName = "ArrowBehavior", menuName = "CombatSystem/Projectile/ProjectileBehaviors/ArrowBehavior", order = 0)]
    public class ArrowBehavior : ProjectileBehavior
    {
        public override void ApplyEffect(IDamageable target)
        {
            // throw new System.NotImplementedException();
        }
    }
}
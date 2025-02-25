using _Game.Interfaces;
using UnityEngine;

namespace _Game.GameMechanics
{
    [CreateAssetMenu(fileName = "ArrowBehavior", menuName = "CombatSystem/Projectile/ProjectileBehaviors/ArrowBehavior", order = 0)]
    public class ArrowBehavior : ProjectileBehavior
    {
        public override void ApplyEffect(Projectile projectile, IDamageable target)
        {
            // throw new System.NotImplementedException();
        }
    }
}
using _Game.CombatSystem;
using _Game.SkillSystem;
using UnityEngine;

namespace _Game.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
        Vector3 GetPosition();
        void ApplyProjectileEffect(ProjectileBehavior projectileBehavior);
    }
}
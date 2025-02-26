using _Game.CombatSystem;
using _Game.Enums;
using _Game.SkillSystem;
using UnityEngine;

namespace _Game.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
        Vector3 GetPosition();
        void ApplyStatusEffect(StatusEffectType statusEffect, float effectDuration = 0, float effectValue = 0);
    }
}
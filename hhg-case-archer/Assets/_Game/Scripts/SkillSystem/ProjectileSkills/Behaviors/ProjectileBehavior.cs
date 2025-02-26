using _Game.CombatSystem;
using _Game.Interfaces;
using UnityEngine;

namespace _Game.SkillSystem
{
    public abstract class ProjectileBehavior : ScriptableObject
    {
        public abstract void ApplyEffect(Projectile projectile, IDamageable target);
    }
}
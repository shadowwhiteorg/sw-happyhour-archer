using _Game.Interfaces;
using UnityEngine;

namespace _Game.CombatSystem
{
    public abstract class ProjectileBehavior : ScriptableObject
    {
        public abstract void ApplyEffect(IDamageable target);
    }
}
using _Game.Interfaces;
using UnityEngine;

namespace _Game.GameMechanics
{
    public abstract class ProjectileBehavior : ScriptableObject
    {
        public abstract void ApplyEffect(Projectile projectile, IDamageable target);
    }
}
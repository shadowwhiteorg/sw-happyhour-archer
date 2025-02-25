using System.Collections.Generic;
using _Game.Enums;
using UnityEngine;

namespace _Game.GameMechanics
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "CombatSystem/Projectile/Projectile Data", order = 0)]
    public class ProjectileData : ScriptableObject
    {
        public ProjectileType Type;
        public GameObject Prefab;
        public float Speed;
        public float Damage;
        public List<ProjectileBehavior> DefaultBehaviors;
    }
}
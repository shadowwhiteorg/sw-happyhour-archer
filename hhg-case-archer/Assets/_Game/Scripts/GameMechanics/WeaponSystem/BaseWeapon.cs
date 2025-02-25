using System.Collections.Generic;
using _Game.Core;
using _Game.Enums;
using _Game.Interfaces;
using UnityEngine;
using _Game.Utils;
using UnityEngine.Serialization;

namespace _Game.GameMechanics
{
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private List<ProjectileData> allProjectiles;
        [SerializeField] private List<ProjectileBehavior> extraBehaviors;
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private ProjectileData defaultProjectile;
        
        public float ShootingSpeed => weaponData.ShootingSpeed;
        public float AttackRate => weaponData.AttackRate;
        public Transform FirePoint => firePoint;
        private BaseCharacter _owner;
        public BaseCharacter Owner => _owner;
        private Dictionary<ProjectileType, ObjectPool<Projectile>> _pools = new Dictionary<ProjectileType, ObjectPool<Projectile>>();
        private ProjectileData _activeProjectile; // Stores the current projectile type
        public ProjectileData ActiveProjectile => _activeProjectile;

        public IDamageable CurrentTarget => CombatManager.Instance.FindNearestEnemy(transform.position, 20);

        private void Awake()
        {
            InitializeProjectilePools();
            SetActiveProjectile(ProjectileType.Default); // Default projectile type
        }

        public void Initialize(BaseCharacter character)
        {
            _owner = character;
        }
        
        private void InitializeProjectilePools()
        {
            foreach (var projectileData in allProjectiles)
            {
                if (!_pools.ContainsKey(projectileData.Type))
                {
                    _pools[projectileData.Type] = new ObjectPool<Projectile>(projectileData.Prefab.GetComponent<Projectile>(), 10,this.transform);
                }
            }
        }

        public void Attack()
        {
            if (_activeProjectile == null)
                _activeProjectile = defaultProjectile;

            List<ProjectileBehavior> behaviors = new List<ProjectileBehavior>(_activeProjectile.DefaultBehaviors);
            behaviors.AddRange(extraBehaviors);

            if (_pools.TryGetValue(_activeProjectile.Type, out var pool))
            {
                Projectile projectile = pool.Get();
                projectile.Initialize(this, behaviors, pool);
                projectile.transform.position = firePoint.position;
                projectile.transform.rotation = firePoint.rotation;
                projectile.Launch();
            }
        }

        public void SetActiveProjectile(ProjectileType newType)
        {
            ProjectileData newProjectile = allProjectiles.Find(p => p.Type == newType);
            if (newProjectile != null)
            {
                _activeProjectile = newProjectile;
                Debug.Log($"Weapon switched to: {newType}");
            }
        }

        public void AddExtraBehavior(ProjectileBehavior behavior)
        {
            if (!extraBehaviors.Contains(behavior))
                extraBehaviors.Add(behavior);
        }
    }
}
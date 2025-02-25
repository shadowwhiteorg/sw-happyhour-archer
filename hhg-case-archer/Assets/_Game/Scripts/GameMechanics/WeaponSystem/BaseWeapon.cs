using System.Collections.Generic;
using _Game.Core;
using _Game.Enums;
using _Game.Interfaces;
using UnityEngine;
using _Game.Utils;
namespace _Game.GameMechanics
{
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private List<ProjectileData> _allProjectiles;
        [SerializeField] private List<ProjectileBehavior> _extraBehaviors;
        [SerializeField] private WeaponData _weaponData;
        
        public float ShootingSpeed => _weaponData.ShootingSpeed;
    
        private Dictionary<ProjectileType, ObjectPool<Projectile>> _pools = new Dictionary<ProjectileType, ObjectPool<Projectile>>();
        private ProjectileData _activeProjectile; // Stores the current projectile type
        public ProjectileData ActiveProjectile => _activeProjectile;

        public IDamageable CurrentTarget => CombatManager.Instance.FindNearestEnemy(transform.position, 20);

        private void Awake()
        {
            InitializeProjectilePools();
            SetActiveProjectile(ProjectileType.Default); // Default projectile type
        }

        private void InitializeProjectilePools()
        {
            foreach (var projectileData in _allProjectiles)
            {
                if (!_pools.ContainsKey(projectileData.Type))
                {
                    _pools[projectileData.Type] = new ObjectPool<Projectile>(projectileData.Prefab.GetComponent<Projectile>(), 10);
                }
            }
        }

        public void Attack()
        {
            if (_activeProjectile == null) return;

            List<ProjectileBehavior> behaviors = new List<ProjectileBehavior>(_activeProjectile.DefaultBehaviors);
            behaviors.AddRange(_extraBehaviors);

            if (_pools.TryGetValue(_activeProjectile.Type, out var pool))
            {
                Projectile projectile = pool.Get();
                projectile.Initialize(this, behaviors);
                projectile.transform.position = _firePoint.position;
                projectile.transform.rotation = _firePoint.rotation;
            }
        }

        public void SetActiveProjectile(ProjectileType newType)
        {
            ProjectileData newProjectile = _allProjectiles.Find(p => p.Type == newType);
            if (newProjectile != null)
            {
                _activeProjectile = newProjectile;
                Debug.Log($"Weapon switched to: {newType}");
            }
        }

        public void AddExtraBehavior(ProjectileBehavior behavior)
        {
            if (!_extraBehaviors.Contains(behavior))
                _extraBehaviors.Add(behavior);
        }
    }
}
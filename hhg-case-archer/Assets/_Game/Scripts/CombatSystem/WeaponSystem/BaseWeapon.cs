using System;
using System.Collections.Generic;
using _Game.Managers;
using _Game.Enums;
using _Game.Interfaces;
using _Game.SkillSystem;
using UnityEngine;
using _Game.Utils;
using UnityEngine.Serialization;

namespace _Game.CombatSystem
{
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private List<ProjectileData> allProjectiles;
        [FormerlySerializedAs("extraBehaviors")] [SerializeField] private List<ProjectileBehavior> extraProjectileBehaviors;
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private ProjectileData defaultProjectile;
        
        public float ShootingSpeed => weaponData.ShootingSpeed;
        private List<Projectile> _activeProjectiles = new List<Projectile>();
        public float AttackRate => weaponData.AttackRate;
        public Transform FirePoint => firePoint;
        private BaseCharacter _owner;
        public BaseCharacter Owner => _owner;
        private Dictionary<ProjectileType, ObjectPool<Projectile>> _pools = new Dictionary<ProjectileType, ObjectPool<Projectile>>();
        private ProjectileData _activeProjectileData; // Stores the current projectile type
        public ProjectileData ActiveProjectileData => _activeProjectileData;
        private IDamageable _currentTarget;
        public IDamageable CurrentTarget => _currentTarget;

        private void Awake()
        {
            InitializeProjectilePools();
            SetActiveProjectile(ProjectileType.Default); // Default projectile type
        }

        public void Initialize(BaseCharacter character)
        {
            _owner = character;
        }

        private void OnEnable()
        {
            EventManager.OnEnemyDeath += ClearActiveProjectiles;
            EventManager.OnEnemyDeath += SetCurrentTarget;
        }

        private void OnDisable()
        {
            EventManager.OnEnemyDeath -= ClearActiveProjectiles;
            EventManager.OnEnemyDeath -= SetCurrentTarget;
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

        public void SetCurrentTarget()
        {
            _currentTarget = CombatManager.Instance.FindNearestEnemy(transform.position, 20);
        }

        public void Attack()
        {
            if (_activeProjectileData == null)
                _activeProjectileData = defaultProjectile;

            List<ProjectileBehavior> projectileBehaviors = new List<ProjectileBehavior>(_activeProjectileData.DefaultBehaviors);
            projectileBehaviors.AddRange(extraProjectileBehaviors);

            if (_pools.TryGetValue(_activeProjectileData.Type, out var pool))
            {
                Projectile projectile = pool.Get();
                projectile.Initialize(this, projectileBehaviors, pool);
                AddToActiveProjectiles(projectile);
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
                _activeProjectileData = newProjectile;
                Debug.Log($"Weapon switched to: {newType}");
            }
        }
        
        

        public void AddExtraProjectileBehavior(ProjectileBehavior behavior)
        {
            if (!extraProjectileBehaviors.Contains(behavior))
                extraProjectileBehaviors.Add(behavior);
        }
        
        public void RemoveProjectileBehavior(ProjectileBehavior behavior)
        {
            if (extraProjectileBehaviors.Contains(behavior))
                extraProjectileBehaviors.Remove(behavior);
        }
        
        private void AddToActiveProjectiles(Projectile projectile)
        {
            _activeProjectiles.Add(projectile);
        }
        
        public void RemoveFromActiveProjectiles(Projectile projectile)
        {
            _activeProjectiles.Remove(projectile);
        }
        private void ClearActiveProjectiles()
        {
            List<Projectile> toRemove = new List<Projectile>();
            toRemove.AddRange(_activeProjectiles);
            foreach (var projectile in toRemove)
            {
                projectile?.ReturnToPool();
            }
        }
    }
}
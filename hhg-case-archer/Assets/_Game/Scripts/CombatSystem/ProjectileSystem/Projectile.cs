using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Interfaces;
using _Game.Managers;
using _Game.Utils;
using Unity.VisualScripting;

namespace _Game.CombatSystem
{
    public class Projectile : MonoBehaviour
    {
        
        [SerializeField] private Rigidbody rigidbody;
        private ObjectPool<Projectile> _pool;
        private Weapon _weapon;
        private IDamageable _target;
        private float _shootingSpeed;
        private float _damage;
        private float _time;
        private int _ricochetCount;
        private bool _usingUnityPhysics;
        private Vector3 _targetPosition;
        private Vector3 _startPosition;
        private List<ProjectileBehavior> _behaviors = new List<ProjectileBehavior>();
        private List<EnemyCharacter> _targetedEnemies = new List<EnemyCharacter>();

        public void Initialize(Weapon weapon, List<ProjectileBehavior> behaviors, ObjectPool<Projectile> sourcePool)
        {
            _weapon = weapon;
            _shootingSpeed = weapon.ShootingSpeed;
            _damage = weapon.ActiveProjectileData.Damage;
            _target = weapon.CurrentTarget;
            _targetPosition = weapon.CurrentTarget.GetPosition();
            _behaviors.Clear();
            _behaviors.AddRange(behaviors);
            _pool = sourcePool;
            EventManager.OnTargetDeath += OnTargetDeath;
        }
        

        public void Launch(float ricochetCount, bool usingUnityPhysics)
        {
            _ricochetCount = (int)ricochetCount;
            _usingUnityPhysics = usingUnityPhysics;
            if (_usingUnityPhysics)
            {
                UnityPhysicsLaunch(_weapon.FirePoint.position, _targetPosition);
            }
            else
            {
                KinematicLaunch(_weapon.FirePoint.position, _targetPosition);
            }
        }


        
        public void UnityPhysicsLaunch(Vector3 origin ,Vector3 target)
        {
            transform.position = origin;
        
            if (!CalculateLaunchVelocity(origin, target, out Vector3 velocity))
            {
                Debug.LogError("Not enough speed to reach the target!");
                return;
            }
        
            rigidbody.linearVelocity = velocity;
        }
        
        private bool CalculateLaunchVelocity(Vector3 start, Vector3 target, out Vector3 velocity)
        {
            velocity = Vector3.zero;
            Vector3 toTarget = target - start;
            float horizontalDistance = new Vector3(toTarget.x, 0, toTarget.z).magnitude;
            float heightDifference = target.y - start.y;
            float gravity = Mathf.Abs(Physics.gravity.y);
        
            float minSpeedRequired = Mathf.Sqrt(gravity * horizontalDistance * horizontalDistance / (2 * heightDifference));
            if (_shootingSpeed < minSpeedRequired)
            {
                _shootingSpeed = minSpeedRequired;
            }
        
            float termInsideSqrt = (_shootingSpeed * _shootingSpeed * _shootingSpeed * _shootingSpeed) -
                                   gravity * (gravity * horizontalDistance * horizontalDistance + 2 * heightDifference * _shootingSpeed * _shootingSpeed);
        
            if (termInsideSqrt < 0)
                return false;
        
            float theta = Mathf.Atan((_shootingSpeed * _shootingSpeed + Mathf.Sqrt(termInsideSqrt)) / (gravity * horizontalDistance));
        
            float vx = _shootingSpeed * Mathf.Cos(theta);
            float vy = _shootingSpeed * Mathf.Sin(theta);
        
            velocity = new Vector3(toTarget.x / horizontalDistance * vx, vy, toTarget.z / horizontalDistance * vx);
            return true;
        }
        
        
        private void KinematicLaunch(Vector3 origin, Vector3 target)
        {
            _startPosition = origin;
            _targetPosition = target;
            transform.position = _startPosition;
            _time = 0f;
        
            if (!CalculateLaunchVelocity(out Vector3 velocity))
            {
                Debug.LogError("Not enough speed to reach the target!");
                return;
            }
            if(this.gameObject.activeSelf)
                StartCoroutine(KinematicMovementCoroutine(velocity, _targetPosition, _target));
        }
        
        private bool CalculateLaunchVelocity(out Vector3 velocity)
        {
            velocity = Vector3.zero;
            Vector3 toTarget = _targetPosition - _startPosition;
            float horizontalDistance = new Vector3(toTarget.x, 0, toTarget.z).magnitude;
            float heightDifference = _targetPosition.y - _startPosition.y;
            float gravity = Mathf.Abs(Physics.gravity.y);
        
            float minSpeedRequired = Mathf.Sqrt(gravity * horizontalDistance * horizontalDistance / (2 * heightDifference));
            if (_shootingSpeed < minSpeedRequired)
            {
                _shootingSpeed = minSpeedRequired;
            }
        
            float termInsideSqrt = (_shootingSpeed * _shootingSpeed * _shootingSpeed * _shootingSpeed) -
                                   gravity * (gravity * horizontalDistance * horizontalDistance + 2 * heightDifference * _shootingSpeed * _shootingSpeed);
        
            if (termInsideSqrt < 0)
                return false;
        
            float theta = Mathf.Atan((_shootingSpeed * _shootingSpeed + Mathf.Sqrt(termInsideSqrt)) / (gravity * horizontalDistance));
        
            float vx = _shootingSpeed * Mathf.Cos(theta);
            float vy = _shootingSpeed * Mathf.Sin(theta);
        
            velocity = new Vector3(toTarget.x / horizontalDistance * vx, vy, toTarget.z / horizontalDistance * vx);
            Debug.Log($"Velocity: {velocity}");
            return true;
        }
        

        IEnumerator KinematicMovementCoroutine(Vector3 velocity, Vector3 target, IDamageable targetDamageable)
        {
            while (Vector3.Distance(transform.position, target) > 1f)
            {
                _time += Time.deltaTime;

                // Apply arc motion using kinematic equations
                Vector3 displacement = velocity * _time + Physics.gravity * (0.5f * (_time * _time));
                transform.position = _startPosition + displacement;
                yield return null;
            }
            transform.position = target;
            HitTarget(targetDamageable);
        }
        
        private void HitTarget(IDamageable target)
        {
            _targetedEnemies.Add((EnemyCharacter)target);
            foreach (var behavior in _behaviors)
            {
                behavior.ApplyEffect(target, _weapon.Owner);
            }
            
            if (_ricochetCount > 0)
            {
                _ricochetCount--;
                var mOldTarget = _target;
                _shootingSpeed = _shootingSpeed = _weapon.ShootingSpeed;
                _target = CombatManager.Instance.FindNearestEnemy(transform.position, 50,(EnemyCharacter)target,_targetedEnemies);
                _targetPosition = _target.GetPosition();
                if(_usingUnityPhysics)
                    UnityPhysicsLaunch(transform.position+Vector3.up*0.1f, _targetPosition);
                else
                    KinematicLaunch(transform.position + Vector3.up*0.1f, _targetPosition);
                mOldTarget.TakeDamage(_damage);
            }
            else
            {
                target.TakeDamage(_damage);
                ReturnToPool();
            }
        }
        

        public void ReturnToPool()
        {
            EventManager.OnTargetDeath -= OnTargetDeath;
            StopAllCoroutines();
            _pool.Return(this);
        }
        
        private void OnTargetDeath(IDamageable target)
        {
            if (target == _target)
            {
                ReturnToPool();
            }
        }
    }
}
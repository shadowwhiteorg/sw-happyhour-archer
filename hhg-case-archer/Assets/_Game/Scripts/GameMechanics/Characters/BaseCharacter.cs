using System;
using _Game.Enums;
using _Game.Interfaces;
using _Game.StatSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.GameMechanics
{
    public class BaseCharacter : MonoBehaviour , IDamageable
    {
        // TODO: Use it untill stat system is implemented
        [SerializeField] private StatConfig statConfig;
        private float _baseHealth = 100;
        public float Health => StatController.GetStatValue(StatType.Health);
        
        public CharacterState CharacterState;

        public StatController StatController;
    
        public MovingActor MovingActor => GetComponent<MovingActor>();
        public AttackingActor AttackingActor => GetComponent<AttackingActor>();

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            StatController = new StatController(statConfig);
            _baseHealth = Health;
        }
    
        public float GetDamage()
        {
            // Modify after stat system is implemented
            return StatController.GetStatValue(StatType.AttackDamage);
        }


        public void TakeDamage(float damage)
        {
            _baseHealth -= damage;
            if (_baseHealth <= 0)
            {
                Die();
            }
        }

        public virtual Vector3 GetPosition()
        {
            return transform.position;
        }
    
        public void ApplyProjectileEffect(ProjectileBehavior projectileBehavior)
        {
            // throw new System.NotImplementedException();
        }
        
        protected virtual void Die()
        {
            //Destroy(gameObject);
        }
    }
}
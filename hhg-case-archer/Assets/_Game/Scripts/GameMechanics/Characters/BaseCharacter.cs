using _Game.Enums;
using _Game.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.GameMechanics
{
    public class BaseCharacter : MonoBehaviour , IDamageable
    {
        // TODO: Use it untill stat system is implemented
        [SerializeField] private float baseDamage;
        [SerializeField] private float baseHealth = 100;
        public float Health => baseHealth;
        
        public CharacterState CharacterState;
    
        public MovingActor MovingActor => GetComponent<MovingActor>();
        public AttackingActor AttackingActor => GetComponent<AttackingActor>();
        
    
        public float GetDamage()
        {
            // Modify after stat system is implemented
            return baseDamage;
        }


        public void TakeDamage(float damage)
        {
            baseHealth -= damage;
            if (Health <= 0)
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
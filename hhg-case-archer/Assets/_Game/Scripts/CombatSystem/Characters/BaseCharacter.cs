using System.Collections.Generic;
using System.Collections;
using _Game.Enums;
using _Game.Interfaces;
using _Game.Managers;
using _Game.SkillSystem;
using _Game.StatSystem;
using UnityEngine;

namespace _Game.CombatSystem
{
    public class BaseCharacter : MonoBehaviour , IDamageable
    {
        [SerializeField] private StatConfig statConfig;
        [SerializeField] private List<BaseSkill> initialSkills;
        [SerializeField] private List<BaseSkill> activeSkills;
        private float _baseHealth = 100;
        private float _burnTimer;
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
            foreach (var skill in initialSkills)
            {
                LearnSkill(skill);
            }
        }
        
        public virtual Vector3 GetPosition()
        {
            return transform.position;
        }

        public void ApplyStatusEffect(StatusEffectType statusEffect, float effectDuration, float effectValue)
        {
            switch (statusEffect)
            {
                case StatusEffectType.Fire:
                    Debug.Log("Burned");
                    _burnTimer = 0;
                    StartCoroutine(ExtendedTakeDamage());
                    break;
            }
            
        }
        
        private IEnumerator ExtendedTakeDamage()
        {
           
            while(_burnTimer <= StatController.GetStatValue(StatType.BurnDamageDuration))
            {
                for (int i = 0; i < 100; i++)
                {
                    Debug.Log("Burning" + StatController.GetStatValue(StatType.BurnDamage));
                    // TakeDamage(StatController.GetStatValue(StatType.BurnDamage));
                    TakeDamage(12);
                    yield return new WaitForSeconds(1f);
                }
                _burnTimer += Time.deltaTime;
            }
            yield return null;
        }

        public float GetDamage()
        {
            return StatController.GetStatValue(StatType.AttackDamage);
        }

        public void TakeDamage(float damage)
        {
            Debug.Log("Taking Damage "+damage);
            _baseHealth -= damage;
            if (_baseHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            EventManager.FireOnTargetDeath(this);
        }
        
        public void LearnSkill(BaseSkill skill)
        {
            skill.ApplySkill(this);
            activeSkills.Add(skill);
        }

        public void RemoveSkill(BaseSkill skill)
        {
            skill.RemoveSkill(this);
            activeSkills.Remove(skill);
        }
        
    }
}
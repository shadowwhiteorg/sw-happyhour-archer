using System.Collections;
using _Game.Core;
using _Game.Interfaces;
using UnityEngine;

namespace _Game.GameMechanics
{
    
    public class AttackingActor : ActorComponent
    {
        [SerializeField] private BaseWeapon weapon;
        public override void Initialize(BaseCharacter character)
        {
            base.Initialize();
        }
        
        public void Attack()
        {
            // Set Search radius ( = 20 ) to Attack Range -> StatSystem Implementation
            weapon.Attack();
        }

        public void StartAttack()
        {
            Debug.Log("AttackingActor StartAttack");
            StartCoroutine(RepeatedAttack());
        }
        
        public void Stop()
        {
            Debug.Log("AttackingActor Stop");
            StopAllCoroutines();
        }
        
        IEnumerator RepeatedAttack()
        {
            while (true)
            {
                Attack();
                yield return new WaitForSeconds(_character.AttackingActor.weapon.AttackRate);
            }
        }
        
    }
}
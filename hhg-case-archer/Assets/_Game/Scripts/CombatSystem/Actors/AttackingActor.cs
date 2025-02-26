using System.Collections;
using UnityEngine;

namespace _Game.CombatSystem
{
    
    public class AttackingActor : ActorComponent
    {
        [SerializeField] private Weapon weapon;
        public Weapon Weapon => weapon;
        public override void Initialize(BaseCharacter character)
        {
            base.Initialize();
            weapon.Initialize(character);
        }
        
        public void Attack()
        {
            weapon.Attack();
        }

        public void StartAttack()
        {
            weapon.SetCurrentTarget();
            StartCoroutine(RepeatedAttack());
        }
        
        public void Stop()
        {
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
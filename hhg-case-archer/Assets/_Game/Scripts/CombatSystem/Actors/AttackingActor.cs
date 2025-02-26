using System.Collections;
using UnityEngine;

namespace _Game.CombatSystem
{
    
    public class AttackingActor : ActorComponent
    {
        [SerializeField] private BaseWeapon weapon;
        public BaseWeapon Weapon => weapon;
        public override void Initialize(BaseCharacter character)
        {
            base.Initialize();
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
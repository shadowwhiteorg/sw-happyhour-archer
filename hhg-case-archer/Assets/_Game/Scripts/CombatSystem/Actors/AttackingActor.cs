using System.Collections;
using _Game.Enums;
using _Game.Utils;
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
                for (int i = 0; i < _character.StatController.GetStatValue(StatType.AttackCount); i++)
                {
                    Attack();
                    yield return new WaitForSeconds(0.2f);
                }
                
                yield return new WaitForSeconds(1/(_character.StatController.GetStatValue(StatType.AttackSpeed).FloatMap(10, 30, 1, 4)));
            }
        }
        
    }
}
using System.Collections;
using _Game.Enums;
using _Game.Utils;
using UnityEngine;

namespace _Game.CombatSystem
{
    
    public class AttackingActor : ActorComponent
    {
        [SerializeField] private Weapon weapon;
        private BaseCharacter _character;
        public Weapon Weapon => weapon;
        public override void Initialize(BaseCharacter character)
        {
            base.Initialize();
            _character = character;
            weapon.Initialize(character);
        }
        
        public void Attack()
        {
            weapon.Attack();
        }

        public void StartAttack()
        {
            weapon.SetCurrentTarget();
            _character.MovingActor.NavMeshAgent.updateRotation = false;
            _character.transform.LookAt(weapon.CurrentTarget.GetPosition());
            _character.MovingActor.NavMeshAgent.updateRotation = true;
            StartCoroutine(RepeatedAttack());
            //StartCoroutine(RotateTowardsTarget(weapon.CurrentTarget.GetPosition()));
        }
        
        private IEnumerator RotateTowardsTarget(Vector3 targetPosition)
        {
            _character.MovingActor.NavMeshAgent.updateRotation = false;
            while (true)
            {
                if (targetPosition == _character.transform.position) yield break;

                Vector3 direction = (targetPosition - _character.transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Ignore Y-axis

                _character.transform.rotation = Quaternion.Slerp(
                    _character.transform.rotation,
                    targetRotation,
                    Time.deltaTime * 120f
                );
                _character.MovingActor.NavMeshAgent.updateRotation = true;
                StartCoroutine(RepeatedAttack());
                yield return null;
                
            }
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
                _character.CharacterModel.PlayAttackAnimation();
                // yield return new WaitForSeconds(1);
                float delay = 1 / (_character.StatController.GetStatValue(StatType.AttackSpeed).Map(15, 60, 2, 1));
                Debug.Log("Delay: "+delay);
                yield return new WaitForSeconds(delay);
            }
        }
        
    }
}
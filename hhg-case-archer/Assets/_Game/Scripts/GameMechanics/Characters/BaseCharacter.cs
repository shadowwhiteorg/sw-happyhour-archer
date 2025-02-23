using _Game.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.GameMechanics
{
    public class BaseCharacter : MonoBehaviour
    {
        [SerializeField] private bool hasMovement;
        [SerializeField] private bool hasAttack;
        public CharacterState CharacterState;
        
        [HideInInspector]
        public MovingActor MovingActor;
        [HideInInspector]
        public AttackingActor AttackingActor;
        
        protected virtual void Awake()
        {
            if(hasMovement)
                MovingActor = gameObject.AddComponent<MovingActor>();
            if(hasAttack)
                AttackingActor = gameObject.AddComponent<AttackingActor>();
        }
    }
}
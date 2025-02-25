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

    [SerializeField] private bool hasMovement;
    [SerializeField] private bool hasAttack;
    public CharacterState CharacterState;

    public MovingActor MovingActor => GetComponent<MovingActor>();
    public AttackingActor AttackingActor => GetComponent<AttackingActor>();

    protected virtual void Awake()
    {
        // if (hasMovement)
        //     MovingActor = gameObject.AddComponent<MovingActor>();
        // if (hasAttack)
        //     AttackingActor = gameObject.AddComponent<AttackingActor>();
    }

    public float GetDamage()
    {
        // Modify after stat system is implemented
        return baseDamage;
    }

    public void TakeDamage(float damage)
    {
        // throw new System.NotImplementedException();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void ApplyProjectileEffect(ProjectileBehavior projectileBehavior)
    {
        // throw new System.NotImplementedException();
    }
    }
}
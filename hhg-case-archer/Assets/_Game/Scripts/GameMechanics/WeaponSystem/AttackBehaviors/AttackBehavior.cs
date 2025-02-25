using UnityEngine;

namespace _Game.GameMechanics
{
    public abstract class AttackBehavior : ScriptableObject
    {
        public abstract void ExecuteAttack(BaseWeapon weapon, Vector3 targetPosition);
    }
}
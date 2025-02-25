using _Game.Core;
using _Game.Interfaces;
using UnityEngine;

namespace _Game.GameMechanics
{
    public class EnemyCharacter : BaseCharacter
    {
        protected override void Die()
        {
            base.Die();
            CombatManager.Instance.RemoveEnemy(this);
            EventManager.FireOnEnemyDeath();
            Destroy(gameObject);
        }
        
    }
}
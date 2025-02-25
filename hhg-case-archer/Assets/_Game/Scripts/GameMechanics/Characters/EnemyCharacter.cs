using _Game.Interfaces;
using UnityEngine;

namespace _Game.GameMechanics
{
    public class EnemyCharacter : BaseCharacter
    {
        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}
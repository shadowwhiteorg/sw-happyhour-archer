using System;
using _Game.GameMechanics;
using UnityEngine;

namespace _Game.Core
{
    public static class EventManager
    {
        public static event Action<BaseCharacter> OnMoveStart;
        public static event Action<BaseCharacter> OnMoveEnd;
        public static event Action OnSearchEnemies;
        public static event Action OnEnemyDeath;
        
        public static void FireOnMoveStart(BaseCharacter character)
        {
            Debug.Log("FireOnMoveStart");
            OnMoveStart?.Invoke(character);
        }
        public static void FireOnMoveEnd(BaseCharacter character)
        {
            OnMoveEnd?.Invoke(character);
        }

        public static void FireOnEnemySearch()
        {
            OnSearchEnemies?.Invoke();
        }
        
        public static void FireOnEnemyDeath()
        {
            OnEnemyDeath?.Invoke();
        }
        
    }
}
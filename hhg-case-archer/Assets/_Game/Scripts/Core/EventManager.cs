using System;
using _Game.GameMechanics;
using UnityEngine;

namespace _Game.Core
{
    public static class EventManager
    {
        public static event Action<BaseCharacter> OnMoveStart;
        public static event Action<BaseCharacter> OnMoveEnd;
        
        public static void FireOnMoveStart(BaseCharacter character)
        {
            Debug.Log("FireOnMoveStart");
            OnMoveStart?.Invoke(character);
        }
        public static void FireOnMoveEnd(BaseCharacter character)
        {
            OnMoveEnd?.Invoke(character);
        }
        
        
    }
}
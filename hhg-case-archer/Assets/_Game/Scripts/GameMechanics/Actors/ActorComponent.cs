using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.GameMechanics
{
    public class ActorComponent : MonoBehaviour
    {

        private BaseCharacter _character;
        public BaseCharacter Character => _character;
        
        [HideInInspector]
        public bool IsInitialized;
        public virtual void Initialize(BaseCharacter character)
        {
            if (IsInitialized)
                return;
            _character = character;
            IsInitialized = true;
        }
    }
}
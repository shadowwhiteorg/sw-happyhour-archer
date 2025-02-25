using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.GameMechanics
{
    [RequireComponent(typeof(BaseCharacter))]
    public class ActorComponent : MonoBehaviour
    {

        protected BaseCharacter _character;
        public BaseCharacter Character => _character;
        
        [HideInInspector]
        public bool IsInitialized;
        public virtual void Initialize(BaseCharacter character = null)
        {
            if (IsInitialized)
                return;
            _character = GetComponent<BaseCharacter>();
            IsInitialized = true;
        }
    }
}
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.GameMechanics
{
    public class ActorComponent : MonoBehaviour
    {
        [HideInInspector]
        public bool IsInitialized;
        public virtual void Initialize()
        {
            if (IsInitialized)
                return;
            IsInitialized = true;
        }
    }
}
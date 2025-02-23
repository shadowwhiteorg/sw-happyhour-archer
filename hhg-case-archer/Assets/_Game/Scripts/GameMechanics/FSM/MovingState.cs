using _Game.Interfaces;
using UnityEngine;

namespace _Game.GameMechanics
{
    public class MovingState : ICharacterState
    {
        public void EnterState(BaseCharacter character)
        {
            character.MovingActor?.Initialize();
        }

        public void UpdateState(BaseCharacter character)
        {
            
        }

        public void ExitState(BaseCharacter character)
        {
            character.MovingActor?.Stop();
        }
    }
}
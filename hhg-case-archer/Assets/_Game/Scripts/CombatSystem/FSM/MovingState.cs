using _Game.Interfaces;
using UnityEngine;

namespace _Game.CombatSystem
{
    public class MovingState : ICharacterState
    {
        public void EnterState(BaseCharacter character)
        {
            character.MovingActor?.Initialize(character);
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
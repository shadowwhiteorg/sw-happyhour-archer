using _Game.Interfaces;
using UnityEngine;

namespace _Game.GameMechanics
{
    public class AttackingState : ICharacterState
    {
        public void EnterState(BaseCharacter character)
        {
            character.AttackingActor?.Initialize(character);
        }

        public void UpdateState(BaseCharacter character)
        {
            
        }

        public void ExitState(BaseCharacter character)
        {
            
        }
    }
}
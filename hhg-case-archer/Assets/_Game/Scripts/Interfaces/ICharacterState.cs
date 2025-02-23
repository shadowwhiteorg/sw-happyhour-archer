using _Game.GameMechanics;

namespace _Game.Interfaces
{
    public interface ICharacterState
    {
        void EnterState(BaseCharacter character);
        void UpdateState(BaseCharacter character);
        void ExitState(BaseCharacter character);
    }
}
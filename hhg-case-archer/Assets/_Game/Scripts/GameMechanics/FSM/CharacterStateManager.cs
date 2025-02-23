using System;
using _Game.Interfaces;
using _Game.Utils;
using UnityEngine;
using static _Game.Core.EventManager;

namespace _Game.GameMechanics
{
    public class CharacterStateManager : MonoBehaviour
    {
        private ICharacterState _currentState;
        private BaseCharacter _character;

        private void Awake()
        {
            _character = GetComponent<BaseCharacter>();
            SetState(new IdleState());
        }

        private void OnEnable()
        {
            OnMoveStart += _ => SetState(new MovingState());
            OnMoveEnd += _ => SetState(new AttackingState());
        }
        
        private void OnDisable()
        {
            OnMoveStart -= _ => SetState(new MovingState());
            OnMoveEnd -= _ => SetState(new AttackingState());
        }
        

        private void SetState(ICharacterState newState)
        {
            if (_currentState != null)
                _currentState.ExitState(_character);
            _currentState = newState;
            _currentState.EnterState(_character);
        }
        
        
        
        
        // Use it if only it's necessary
        // private void Update()
        // {
        //     _currentState?.UpdateState(_character);
        // }
        
        
    }
}
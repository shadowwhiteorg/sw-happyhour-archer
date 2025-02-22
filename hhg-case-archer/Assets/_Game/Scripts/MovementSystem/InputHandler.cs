using _Game.Utils;
using UnityEngine;

namespace _Game.MovementSystem
{
    public class InputHandler : Singleton<InputHandler>
    {
        private Camera _mainCamera;

        protected override void Awake()
        {
            base.Awake();
            _mainCamera = Camera.main;
        }   
        
        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if(Input.GetMouseButtonDown(0))
            {
                JoystickHandler.Instance.ShowJoystick(Input.mousePosition);
            }
            else if(Input.GetMouseButton(0))
            {
                JoystickHandler.Instance.UpdateJoystickPosition(Input.mousePosition);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                JoystickHandler.Instance.HideJoystick();
            }
        }
        
        
    }
}
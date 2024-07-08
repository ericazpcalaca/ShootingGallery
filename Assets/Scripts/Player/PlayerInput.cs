using UnityEngine;
using UnityEngine.InputSystem;
using System;
using static ShootingGallery.Input;

namespace ShootingGallery
{
    public class PlayerInput : MonoBehaviour, IPlayerActions
    {
        public Action OnPlayerShoot;
        public Action<Vector2> OnPlayerMoveCamera;

        private Input _input;


        private void Awake()
        {
            _input = new();
            _input.Player.AddCallbacks(this);
            _input.Player.Enable();
        }

        private void OnDestroy()
        {
            _input?.Player.RemoveCallbacks(this);
            _input?.Player.Disable();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed) 
                OnPlayerShoot?.Invoke();
        }

        public void OnMoveCamera(InputAction.CallbackContext context)
        {
            OnPlayerMoveCamera?.Invoke(context.ReadValue<Vector2>());
        }
    }
}
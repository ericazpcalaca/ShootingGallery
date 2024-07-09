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
        private bool _canShoot;

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
            if (_canShoot && context.performed) 
                OnPlayerShoot?.Invoke();
        }

        public void OnMoveCamera(InputAction.CallbackContext context)
        {
            if (_canShoot && context.performed)
                OnPlayerMoveCamera?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnAllowShooting(InputAction.CallbackContext context)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _canShoot = true;
        }

        public void OnPauseShooting(InputAction.CallbackContext context)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _canShoot = false;
        }
    }
}
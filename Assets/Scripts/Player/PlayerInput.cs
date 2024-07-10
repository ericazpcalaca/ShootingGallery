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

            ShowMouse(false);
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
            if (!_canShoot || !context.performed)
                return;

            OnPlayerMoveCamera?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnAllowShooting(InputAction.CallbackContext context)
        {
            if (GameStateManager.Instance.HasGameEnded)
                return;

            ShowMouse(false);
        }

        public void OnPauseShooting(InputAction.CallbackContext context)
        {
            ShowMouse(true);
        }

        public void ShowMouse(bool show)
        {
            Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Confined;
            Cursor.visible = show;
            _canShoot = !show;
        }
    }
}
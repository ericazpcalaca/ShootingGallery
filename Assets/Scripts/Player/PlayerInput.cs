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
        private bool _isPaused;

        private void Awake()
        {
            _input = new();
            _input.Player.AddCallbacks(this);
            _input.Player.Enable();
            _isPaused = false;
            ShowMouse(false);
        }

        private void OnDestroy()
        {
            _input?.Player.RemoveCallbacks(this);
            _input?.Player.Disable();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (_canShoot && context.performed && !_isPaused)
                OnPlayerShoot?.Invoke();
        }

        public void OnMoveCamera(InputAction.CallbackContext context)
        {
            if (!_canShoot || !context.performed || _isPaused)
                return;

            OnPlayerMoveCamera?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnAllowShooting(InputAction.CallbackContext context)
        {
            if (GameStateManager.Instance.HasGameEnded || _isPaused)
                return;

            ShowMouse(false);
        }

        public void OnPauseShooting(InputAction.CallbackContext context)
        {
            _isPaused = true;
            GameStateManager.Instance.GamePause(_isPaused);
            ShowMouse(true);
        }

        public void ShowMouse(bool show)
        {
            Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = show;
            _canShoot = !show;
        }
        public void ResumeGame()
        {
            _isPaused = false;
            GameStateManager.Instance.GamePause(_isPaused);
            ShowMouse(false);
        }

    }
}
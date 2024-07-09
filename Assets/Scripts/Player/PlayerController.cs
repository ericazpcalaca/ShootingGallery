using System;
using UnityEngine;

namespace ShootingGallery
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraLookAt;
        [SerializeField] private Transform _targetIndicator;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _raycastDistance = 15f;
        [SerializeField] private float _cameraRotationSpeed = 1;
        [SerializeField] private float _minRotDelta = 0.3f;
        [SerializeField] private float _minPitch; 
        [SerializeField] private float _maxPitch;  
        [SerializeField] private float _minYaw;   
        [SerializeField] private float _maxYaw ;
        [SerializeField] private bool _debugEnabled;

        public static Action<uint> UpdateScore;

        private PlayerInput _playerInput;
        private float _currentCameraYaw;
        private float _currentCameraPitch;
        private uint _playerScore;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.OnPlayerShoot += OnPlayerShoot;
            _playerInput.OnPlayerMoveCamera += OnPlayerMoveCamera;
            _playerScore = 0;

            CalculateInitialCameraRotation();

            GameStateManager.OnGameEnd += HandleGameEnd;
        }

        private void OnDestroy()
        {
            _playerInput.OnPlayerShoot -= OnPlayerShoot;
            _playerInput.OnPlayerMoveCamera -= OnPlayerMoveCamera;
            GameStateManager.OnGameEnd -= HandleGameEnd;
        }

        private void Update()
        {
            UpdateRotation();
        }

        private void OnPlayerShoot()
        {
            Vector3 startPos = _targetIndicator.position + _targetIndicator.forward * 3f;
            
            if (_debugEnabled)
                Debug.DrawLine(startPos, _targetIndicator.forward * _raycastDistance, Color.cyan, 3);

            if (Physics.Raycast(startPos, _targetIndicator.forward, out RaycastHit hitInfo, _raycastDistance, _targetLayer.value))
            {
                GameObject target = hitInfo.collider.gameObject;
                var targetMovement = target.GetComponent<TargetController>();
                _playerScore += targetMovement.TargetScore;

                UpdateScore?.Invoke(_playerScore);

                TargetManager.Instance.ReturnToPool(target);
            }
        }

        private void OnPlayerMoveCamera(Vector2 posDelta)
        {
            if (Mathf.Abs(posDelta.x) > _minRotDelta)
            {
                _currentCameraYaw += posDelta.x * _cameraRotationSpeed * Time.deltaTime;
            }

            if (Mathf.Abs(posDelta.y) > _minRotDelta)
            {
                _currentCameraPitch += (posDelta.y * -1) * _cameraRotationSpeed * Time.deltaTime;
            }

            if (_debugEnabled)
                Debug.Log($"Current pitch {_currentCameraPitch} Current yaw: {_currentCameraYaw}");
        }

        private void CalculateInitialCameraRotation()
        {
            Vector3 lookDirection = (_cameraLookAt.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(lookDirection);
            _currentCameraYaw = transform.eulerAngles.y;
            _currentCameraPitch = transform.eulerAngles.x;
        }

        private void UpdateRotation()
        {
            // Clamp the yaw and pitch angles to the specified ranges
            _currentCameraYaw = Mathf.Clamp(_currentCameraYaw, _minYaw, _maxYaw);
            _currentCameraPitch = Mathf.Clamp(_currentCameraPitch, _minPitch, _maxPitch);

            // Player camera will follow this target
            transform.rotation = Quaternion.Euler(_currentCameraPitch, _currentCameraYaw, 0.0f);
        }

        private void HandleGameEnd()
        {
            Debug.Log("PlayerController received game end notification.");
        }
    }
}

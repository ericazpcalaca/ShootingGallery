using UnityEngine;
using static UnityEngine.UI.Image;

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
        [SerializeField] private bool _debugEnabled;

        private PlayerInput _playerInput;
        private float _currentCameraYaw;
        private float _currentCameraPitch;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.OnPlayerShoot += OnPlayerShoot;
            _playerInput.OnPlayerMoveCamera += OnPlayerMoveCamera;

            CalculateInitialCameraRotation();
        }

        private void OnDestroy()
        {
            _playerInput.OnPlayerShoot -= OnPlayerShoot;
            _playerInput.OnPlayerMoveCamera -= OnPlayerMoveCamera;
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
                Destroy(hitInfo.collider.gameObject);
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
            // Clamp rotations so their values are limited to 360 degrees
            _currentCameraYaw = ClampAngle(_currentCameraYaw, float.MinValue, float.MaxValue);
            _currentCameraPitch = ClampAngle(_currentCameraPitch, float.MinValue, float.MaxValue);

            // Player camera will follow this target
            transform.rotation = Quaternion.Euler(_currentCameraPitch, _currentCameraYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}

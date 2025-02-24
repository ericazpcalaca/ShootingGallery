using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootingGallery
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraLookAt;
        [SerializeField] private LayerMask _raycastLayers;
        [SerializeField] private LayerMask _raycastObstructionLayer;
        [SerializeField] private float _raycastDistance = 60f;
        [SerializeField] private float _cameraRotationSpeed = 1;
        [SerializeField] private float _minRotDelta = 0.3f;
        [SerializeField] private float _minPitch;
        [SerializeField] private float _maxPitch;
        [SerializeField] private float _minYaw;
        [SerializeField] private float _maxYaw;
        [SerializeField] private bool _debugEnabled;

        public Action<uint> UpdateScore;
        public Action<uint> UpdateMaxScore;
        public Action<uint> UpdateFinalScore;

        private PlayerInput _playerInput;
        private float _currentCameraYaw;
        private float _currentCameraPitch;
        private uint _playerScore;
        private uint _playerMaxScore;
        private bool _isCountdownActive;
        private float _rotationSpeed = 2.0f;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.OnPlayerShoot += OnPlayerShoot;
            _playerInput.OnPlayerMoveCamera += OnPlayerMoveCamera;
            _playerScore = 0;
            _isCountdownActive = true;

            CalculateInitialCameraRotation();
            GameStateManager.Instance.OnGameCountdown += HandleCountdown;
            GameStateManager.Instance.OnGameStart += HandleStart;
            GameStateManager.Instance.OnGameRestart += HandleRestart;
            GameStateManager.Instance.OnGamePause += HandleGamePause;
            GameStateManager.Instance.OnGameEnd += HandleGameEnd;

            SceneManager.sceneLoaded += OnSceneLoaded;  
        }

        private void OnDestroy()
        {
            _playerInput.OnPlayerShoot -= OnPlayerShoot;
            _playerInput.OnPlayerMoveCamera -= OnPlayerMoveCamera;
            GameStateManager.Instance.OnGameCountdown -= HandleCountdown;
            GameStateManager.Instance.OnGameStart -= HandleStart;
            GameStateManager.Instance.OnGameRestart -= HandleRestart;
            GameStateManager.Instance.OnGamePause -= HandleGamePause;
            GameStateManager.Instance.OnGameEnd -= HandleGameEnd;

            SceneManager.sceneLoaded -= OnSceneLoaded;  
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_debugEnabled)
                Debug.Log("Scene loaded: " + scene.name);

            StartCoroutine(SmoothRotateToLookAt());
            _playerScore = 0;
            UpdateScore?.Invoke(_playerScore);
            _playerInput.ResumeGame();
        }

        private void Update()
        {
            UpdateRotation();
        }

        private void OnPlayerShoot()
        {
            Vector3 direction = transform.forward;
            Vector3 startPos = transform.position;

            if (_debugEnabled)
                Debug.DrawLine(startPos, startPos + (direction * _raycastDistance), Color.cyan, 3);

            if (Physics.Raycast(startPos, direction, out RaycastHit hitInfo, _raycastDistance, _raycastLayers.value))
            {
                if ((_raycastObstructionLayer.value & (1 << hitInfo.collider.gameObject.layer)) != 0)
                    return;

                GameObject targetGameObject = hitInfo.collider.gameObject;
                var target = targetGameObject.GetComponent<Target>();

                if (target.CurrentHit < target.NumberOfHits - 1)
                {
                    target.CurrentHit += 1;
                    TargetManager.Instance.FlashStart(target);
                }
                else
                {
                    target.CurrentHit = 0;
                    _playerScore += target.TargetScore;
                    UpdateScore?.Invoke(_playerScore);
                    AudioManager.Instance.PlayHitTarget();
                    TargetManager.Instance.ReturnToPool(target);
                }
            }
        }

        private void OnPlayerMoveCamera(Vector2 posDelta)
        {
            if (_isCountdownActive)
                return;

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

            if (_debugEnabled)
                Debug.Log($"Initial camera rotation set. Yaw: {_currentCameraYaw}, Pitch: {_currentCameraPitch}");
        }

        private void UpdateRotation()
        {
            // Clamp the yaw and pitch angles to the specified ranges
            _currentCameraYaw = Mathf.Clamp(_currentCameraYaw, _minYaw, _maxYaw);
            _currentCameraPitch = Mathf.Clamp(_currentCameraPitch, _minPitch, _maxPitch);

            // Player camera will follow this target
            transform.rotation = Quaternion.Euler(_currentCameraPitch, _currentCameraYaw, 0.0f);

            if (_debugEnabled)
                Debug.Log($"Updated camera rotation. Yaw: {_currentCameraYaw}, Pitch: {_currentCameraPitch}");
        }

        private void HandleCountdown()
        {
            _isCountdownActive = true;
        }

        private void HandleStart()
        {
            _isCountdownActive = false;
        }

        private void HandleGameEnd()
        {
            _playerMaxScore = (uint)PlayerPrefs.GetInt("HighScore", 0);
            if (_playerScore > _playerMaxScore)
            {
                PlayerPrefs.SetInt("HighScore", (int)_playerScore);
                _playerMaxScore = _playerScore;
                AudioManager.Instance.PlayNewHighScore();
            }
            UpdateMaxScore?.Invoke(_playerMaxScore);
            UpdateFinalScore?.Invoke(_playerScore);
            _playerInput.ShowMouse(true);
            _playerScore = 0;
            UpdateScore?.Invoke(_playerScore);
            StartCoroutine(SmoothRotateToLookAt());
        }

        private void HandleRestart()
        {
            StartCoroutine(SmoothRotateToLookAt());
            _playerScore = 0;
            UpdateScore?.Invoke(_playerScore);
            _playerInput.ResumeGame();
        }

        private IEnumerator SmoothRotateToLookAt()
        {
            Quaternion initialRotation = transform.rotation;
            Vector3 lookDirection = (_cameraLookAt.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            float elapsedTime = 0f;
            while (elapsedTime < 1f / _rotationSpeed)
            {
                transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime * _rotationSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = targetRotation;
            _currentCameraYaw = transform.eulerAngles.y;
            _currentCameraPitch = transform.eulerAngles.x;

            if (_debugEnabled)
                Debug.Log($"Smooth rotate completed. Yaw: {_currentCameraYaw}, Pitch: {_currentCameraPitch}");
        }

        private void HandleGamePause(bool isPaused)
        {
            if (!isPaused)
            {
                _playerInput.ResumeGame();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGallery
{
    public class Target : MonoBehaviour
    {
        public enum MovementType
        {
            Up,
            Down,
            Left,
            Right
        }

        [SerializeField] private MovementType _movementType;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private uint _targetScore;
        [SerializeField] private string _boundaryLayerName;
        [SerializeField] private int _numberOfHits;
        [SerializeField] private float _maxHeight = 5f; 

        private Material _material;
        private Quaternion _originalRotation;
        private Vector3 _startPosition;  

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public MovementType CurrentMovementType
        {
            get { return _movementType; }
            set { _movementType = value; }
        }

        public uint TargetScore
        {
            get { return _targetScore; }
            set { _targetScore = value; }
        }

        public int CurrentHit
        {
            get { return _currentHit; }
            set { _currentHit = value; }
        }

        public int NumberOfHits
        {
            get { return _numberOfHits; }
            set { _numberOfHits = value; }
        }
        public bool CanMove { get; set; }

        private int _boundaryLayer;
        private int _currentHit;

        private void Awake()
        {
            CanMove = true;
            _currentHit = 0;
            _boundaryLayer = LayerMask.NameToLayer(_boundaryLayerName);
            _originalRotation = transform.rotation;
            _startPosition = transform.position;  // Save the starting position

            GameStateManager.Instance.OnGameEnd += HandleGameEnd;
            GameStateManager.Instance.OnGameStart += HandleGameStart;
            GameStateManager.Instance.OnGamePause += HandleGamePause;
            GameStateManager.Instance.OnGameRestart += HandleRestart;

            var meshRenderer = GetComponentInChildren<MeshRenderer>();
            _material = meshRenderer.material;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameEnd -= HandleGameEnd;
            GameStateManager.Instance.OnGameStart -= HandleGameStart;
            GameStateManager.Instance.OnGamePause -= HandleGamePause;
            GameStateManager.Instance.OnGameRestart -= HandleRestart;
        }

        private void Update()
        {
            if (!CanMove)
                return;

            switch (_movementType)
            {
                case MovementType.Up:
                    MoveUp();
                    break;
                case MovementType.Down:
                    MoveDown();
                    break;
                case MovementType.Left:
                    MoveLeft();
                    break;
                case MovementType.Right:
                    MoveRight();
                    break;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == _boundaryLayer)
            {
                TargetManager.Instance.ReturnToPool(this);
            }
        }

        public void ConfigureMaterial(Texture2D currentTexture)
        {
            if (currentTexture == null)
            {
                return;
            }

            _material.mainTexture = currentTexture;
        }

        private void MoveUp()
        {
            transform.position += transform.up * _speed * Time.deltaTime;
            if (transform.position.y >= _startPosition.y + _maxHeight)
            {
                _movementType = MovementType.Down;
            }
        }

        private void MoveDown()
        {
            transform.position += transform.up * _speed * -1 * Time.deltaTime;
        }

        private void MoveLeft()
        {
            transform.position += transform.right * _speed * -1 * Time.deltaTime;
        }

        private void MoveRight()
        {
            Quaternion turn = Quaternion.Euler(0, 180, 0);
            transform.rotation = turn;
            transform.position += transform.right * _speed * -1 * Time.deltaTime;
        }

        private void HandleGameEnd()
        {
            transform.rotation = _originalRotation;
            TargetManager.Instance.ReturnToPool(this);
        }

        private void HandleGameStart()
        {
            transform.rotation = _originalRotation;
            TargetManager.Instance.ReturnToPool(this);
        }

        private void HandleGamePause(bool isPaused)
        {
            CanMove = !isPaused;
        }

        private void HandleRestart()
        {
            transform.rotation = _originalRotation;
            TargetManager.Instance.ReturnToPool(this);
        }
    }
}

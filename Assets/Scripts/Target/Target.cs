using System;
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
            get { return _numberOfHits; }
            set { _numberOfHits = value;  }
        }

        public int NumberOfHits
        {
            get { return _currentHit; }
            set { _currentHit = value; }
        }
        public bool CanMove { get; set; }

        private int _boundaryLayer;
        private int _currentHit;

        private Material _material;

        private void Awake()
        {
            CanMove = true;
            _currentHit = 0;
            _boundaryLayer = LayerMask.NameToLayer(_boundaryLayerName);
            GameStateManager.Instance.OnGameEnd += HandleGameEnd;
            GameStateManager.Instance.OnGameStart += HandleGameStart;
            GameStateManager.Instance.OnGamePause += HandleGamePause;
            GameStateManager.Instance.OnGameRestart += HandleRestart;

            var renderer = gameObject.GetComponent<MeshRenderer>();
            _material = renderer.material;
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

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == _boundaryLayer)
            {
                TargetManager.Instance.ReturnToPool(gameObject);
            }
        }

        private void MoveUp()
        {
            transform.position += transform.up * _speed * Time.deltaTime;
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
            transform.position += _speed * Time.deltaTime * transform.right;
        }

        private void HandleGameEnd()
        {
            TargetManager.Instance.ReturnToPool(gameObject);
        }
        
        private void HandleGameStart()
        {
            TargetManager.Instance.ReturnToPool(gameObject);
        }

        private void HandleGamePause(bool isPaused)
        {
            CanMove = !isPaused;
        }

        private void HandleRestart()
        {
            TargetManager.Instance.ReturnToPool(gameObject); 
        }
    }
}

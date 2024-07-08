using UnityEngine;

namespace ShootingGallery
{
    public class TargetMovement : MonoBehaviour
    {
        public enum MovementType
        {
            UpDown,
            LeftRight
        }

        [SerializeField] private MovementType _movementType;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _leftBoundary = -5f;
        [SerializeField] private float _rightBoundary = 5f;
        [SerializeField] private float _upBoundary = 7;
        [SerializeField] private float _downBoundary = 0;

        public bool CanMove { get; set; }

        private bool _movingRight = true;
        private bool _movingDown = true;

        private void Awake()
        {
            CanMove = true;
        }

        private void Update()
        {
            if (!CanMove)
                return;

            switch (_movementType)
            {
                case MovementType.UpDown:
                    MoveUpDown();
                    break;
                case MovementType.LeftRight:
                    MoveLeftRight();
                    break;
            }
        }

        private void MoveUpDown()
        {
            if (_movingDown)
            {
                transform.position += _speed * Time.deltaTime * transform.up;
                if (transform.position.y >= _upBoundary)
                {
                    _movingDown = false;
                }
            }
            else
            {
                transform.position += _speed * -1 * Time.deltaTime * transform.up;
                if (transform.position.y <= _downBoundary)
                {
                    _movingDown = true;
                }
            }
        }

        private void MoveLeftRight()
        {
            if (_movingRight)
            {
                transform.position += _speed * Time.deltaTime * transform.right;
                if (transform.position.x >= _rightBoundary)
                {
                    _movingRight = false;
                }
            }
            else
            {
                transform.position += _speed * -1 * Time.deltaTime * transform.right;
                if (transform.position.x <= _leftBoundary)
                {
                    _movingRight = true;
                }
            }
        }
    }
}

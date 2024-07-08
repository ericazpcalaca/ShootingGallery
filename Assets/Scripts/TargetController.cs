using UnityEngine;

namespace ShootingGallery
{
    public class TargetController : MonoBehaviour
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
        [SerializeField] private string _boundaryLayerName;

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
        public bool CanMove { get; set; }

        private int _boundaryLayer;

        private void Awake()
        {
            CanMove = true;
            _boundaryLayer = LayerMask.NameToLayer(_boundaryLayerName);
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

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == _boundaryLayer)
            {
                TargetManager.Instance.ReturnToPool(gameObject);
            }
        }
    }
}

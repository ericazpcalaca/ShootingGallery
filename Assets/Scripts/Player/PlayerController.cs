using UnityEngine;

namespace ShootingGallery
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _targetIndicator;
        [SerializeField] private float _raycastDistance = 15f;

        private PlayerInput _input;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _input.OnPlayerShoot += OnPlayerShoot;
        }

        private void OnDestroy()
        {
            _input.OnPlayerShoot -= OnPlayerShoot;
        }

        private void OnPlayerShoot()
        {
            Debug.DrawLine(_targetIndicator.position, _targetIndicator.forward * _raycastDistance, Color.cyan, 3);
            if (Physics.Raycast(_targetIndicator.position, _targetIndicator.forward, _raycastDistance))
            {
                Debug.Log("Hit!!!!");
            }
            else
            {
                Debug.Log("No hit :(");
            }
        }
    }
}

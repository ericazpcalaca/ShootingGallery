using UnityEngine;

namespace ShootingGallery
{
    public class TargetIndicator : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] RectTransform _targetRectTransform;

        void Update()
        {
            var position = transform.position;
            Vector3 screenPosition = _camera.WorldToScreenPoint(position);
            _targetRectTransform.position = screenPosition;
        }
    }
}
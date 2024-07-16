using ShootingGallery;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class TargetScoreDisplay : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _targeScoreDisplay;
    [SerializeField] private TextMeshProUGUI _scoreDisplay;

    public float _offset;
    void Start()
    {
        if (_camera == null)
        {
            _camera = FindObjectOfType<Camera>();
            if (_camera == null)
            {
                Debug.LogError("No Camera found in the scene.");
            }
        }

    }
    void Update()
    {
        if (_camera != null)
        {
            var position = transform.position;
            Vector3 screenPosition = _camera.WorldToScreenPoint(position);
            screenPosition.x += _offset;
            _targeScoreDisplay.position = screenPosition;

            GameObject currentGameObject = gameObject;
            var target = currentGameObject.GetComponent<TargetController>();
            uint targetScore = target.TargetScore;
            _scoreDisplay.text = $"{targetScore}";
        }
    }

}

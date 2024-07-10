using System;
using TMPro;
using UnityEngine;

namespace ShootingGallery
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _maxScoreText;

        PlayerController _playerController;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _playerController.UpdateScore += OnScoreUpdated;
            _playerController.UpdateMaxScore += OnMaxScoreUpdate;
        }

        private void OnDestroy()
        {
            if (_playerController != null)
            {
                _playerController.UpdateScore -= OnScoreUpdated;
                _playerController.UpdateMaxScore -= OnMaxScoreUpdate;
            }
        }

        private void OnScoreUpdated(uint newScore)
        {
            _scoreText.text = $"{newScore}"; 
        }

        private void OnMaxScoreUpdate(uint maxScore)
        {
            _maxScoreText.text = $"{maxScore}";
        }




    }
}
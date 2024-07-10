using TMPro;
using UnityEngine;

namespace ShootingGallery
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        PlayerController _playerController;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _playerController.UpdateScore += OnScoreUpdated;
        }

        private void OnDestroy()
        {
            if (_playerController != null)
            {
                _playerController.UpdateScore -= OnScoreUpdated;
            }
        }

        private void OnScoreUpdated(uint newScore)
        {
            _scoreText.text = $"{newScore}"; 
        }
    }
}
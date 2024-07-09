using TMPro;
using UnityEngine;

namespace ShootingGallery
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText; 

        private void Start()
        {
            PlayerController.UpdateScore += OnScoreUpdated; 
        }

        private void OnDestroy()
        {
            PlayerController.UpdateScore -= OnScoreUpdated;
        }

        private void OnScoreUpdated(uint newScore)
        {
            _scoreText.text = $"{newScore}"; 
        }
    }
}
using System;
using TMPro;
using UnityEngine;

namespace ShootingGallery
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _scoreFinalText;
        [SerializeField] private TextMeshProUGUI _maxScoreText;
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private GameObject _scoreScreen;

        PlayerController _playerController;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _playerController.UpdateScore += OnScoreUpdated;
            _playerController.UpdateMaxScore += OnMaxScoreUpdate;
            _playerController.UpdateFinalScore += OnFinalScoreUpdate;

            GameStateManager.Instance.OnGameStart += OnGameStart;
            GameStateManager.Instance.OnGamePause += OnGamePause;
            GameStateManager.Instance.OnGameEnd += OnGameEnd;
            GameStateManager.Instance.OnGameRestart += OnGameRestart;

            _pauseScreen.SetActive(false);
            _scoreScreen.SetActive(true);

        }

        
        private void OnUpdate()
        {
            if (GameStateManager.Instance.HasGameEnded)
                _pauseScreen.SetActive(false);

            if(GameStateManager.Instance.HasGamePaused)
                _pauseScreen.SetActive(true);
        }

        private void OnDestroy()
        {
            if (_playerController != null)
            {
                _playerController.UpdateScore -= OnScoreUpdated;
                _playerController.UpdateMaxScore -= OnMaxScoreUpdate;
                _playerController.UpdateFinalScore -= OnFinalScoreUpdate;

                GameStateManager.Instance.OnGameStart -= OnGameStart;
                GameStateManager.Instance.OnGamePause -= OnGamePause;
                GameStateManager.Instance.OnGameEnd -= OnGameEnd;
                GameStateManager.Instance.OnGameRestart -= OnGameRestart;
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

        private void OnFinalScoreUpdate(uint finalScore)
        {
            _scoreFinalText.text = $"{finalScore}";
        }

        private void OnGameStart()
        {
            _scoreScreen.SetActive(true);
        }


        private void OnGamePause(bool isPaused)
        {
            _pauseScreen.SetActive(isPaused);
        }

        private void OnGameEnd()
        {
            _pauseScreen.SetActive(false);
            _scoreScreen.SetActive(false);
        }

        private void OnGameRestart()
        {
            _pauseScreen.SetActive(false);
        }

    }
}
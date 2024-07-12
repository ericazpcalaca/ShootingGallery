using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace ShootingGallery
{
    public class PauseSessionButton : MonoBehaviour
    {
        [SerializeField] private Button _btnPauseContinue;
        [SerializeField] private Button _btnPauseRetry;
        [SerializeField] private Button _btnPauseExit;
        [SerializeField] private PlayableDirector _gamePlayableDirector;
        [SerializeField] private PlayableDirector _targetPlayableDirector;

        private PlayerInput _playerInput;
        private CountdownTimer _countdownTimer;

        private void Start()
        {
            _btnPauseContinue.onClick.AddListener(OnPauseContinueButtonClick);
            _btnPauseRetry.onClick.AddListener(OnPauseRetryButtonClick);
            _btnPauseRetry.onClick.AddListener(OnPauseExitButtonClick);       
            
            _playerInput = GetComponentInParent<PlayerInput>();
            _countdownTimer = GetComponentInParent<CountdownTimer>();
        }

        private void OnPauseContinueButtonClick()
        {
            GameStateManager.Instance.GamePause(false);
            _playerInput.ResumeGame();
            _countdownTimer.ResumeCountdown();
        }

        private void OnPauseRetryButtonClick()
        {
            GameStateManager.Instance.GamePause(false);

            _targetPlayableDirector.Stop();
            _targetPlayableDirector.time = 0;

            _gamePlayableDirector.time = 0; 
            _gamePlayableDirector.Play();

            _playerInput.ResumeGame();
            _countdownTimer.RestartCountdown();
            GameStateManager.Instance.EndGame();

        }

        private void OnPauseExitButtonClick()
        {
            Application.Quit();
        }
    }
}
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

        private void Start()
        {
            _btnPauseContinue.onClick.AddListener(OnPauseContinueButtonClick);
            _btnPauseRetry.onClick.AddListener(OnPauseRetryButtonClick);
            _btnPauseRetry.onClick.AddListener(OnPauseExitButtonClick);       
            
        }

        private void OnPauseContinueButtonClick()
        {
            Debug.Log("Continue");
            _gamePlayableDirector.Play();
            _targetPlayableDirector.Play();
            GameStateManager.Instance.GamePause(false);
        }

        private void OnPauseRetryButtonClick()
        {
            _gamePlayableDirector.time = 0;
            _gamePlayableDirector.Play();
            _targetPlayableDirector.time = 0;
            GameStateManager.Instance.GamePause(false);
            GameStateManager.Instance.StartGame();
        }

        private void OnPauseExitButtonClick()
        {
            Application.Quit();
        }
    }
}
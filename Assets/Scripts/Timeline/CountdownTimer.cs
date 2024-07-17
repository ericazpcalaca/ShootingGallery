using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ShootingGallery
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private int _countdownTime;
        [SerializeField] private TextMeshProUGUI _countdownText;
        
        private Coroutine _countdownTimer;
        private WaitForSeconds _waitOneSecond = new WaitForSeconds(1);
        private bool _isCountingDown = false;

        private void Start()
        {
            UpdateCountdownText(_countdownTime);
            GameStateManager.Instance.OnGameStart += StartCountdown;
            GameStateManager.Instance.OnGamePause += HandleGamePause;
            GameStateManager.Instance.OnGameRestart += HandleGameRestart;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStart -= StartCountdown;
            GameStateManager.Instance.OnGamePause -= HandleGamePause;
            GameStateManager.Instance.OnGameRestart -= HandleGameRestart;
        }

        private void UpdateCountdownText(int currentTime)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            _countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void StartCountdown()
        {
            _isCountingDown = true;
            _countdownTimer = StartCoroutine(Countdown(seconds: _countdownTime));
        }

        private IEnumerator Countdown(int seconds)
        {
            int count = seconds;

            while (count >= 0)
            {
                while (!_isCountingDown)
                {
                    yield return null;
                }
                UpdateCountdownText(count);
                yield return _waitOneSecond;
                count--;
            }
        }

        private void HandleGamePause(bool isPaused)
        {
            _isCountingDown = !isPaused;
        }

        private void HandleGameRestart()
        {
            RestartCountdown();
        }

        public void RestartCountdown()
        {
            _isCountingDown = false;
            if (_countdownTimer != null)
            {
                StopCoroutine(_countdownTimer);
            }
            UpdateCountdownText(_countdownTime);
        }      

    }
}

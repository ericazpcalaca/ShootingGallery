using TMPro;
using UnityEngine;

namespace ShootingGallery
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private float _countdownTime;
        [SerializeField] private TextMeshProUGUI _countdownText;

        private float _currentTime;
        private bool _isCountingDown = false;

        private void Start()
        {
            _currentTime = _countdownTime;
            UpdateCountdownText();
            GameStateManager.Instance.OnGameStart += StartCountdown;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStart -= StartCountdown;
        }

        private void Update()
        {
            if (_isCountingDown)
            {
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0)
                {
                    _currentTime = 0;
                    _isCountingDown = false;

                    Debug.Log("Game Over! from Countdown");
                }
                UpdateCountdownText();
            }
        }

        private void UpdateCountdownText()
        {
            int minutes = Mathf.FloorToInt(_currentTime / 60);
            int seconds = Mathf.FloorToInt(_currentTime % 60);
            _countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void StartCountdown()
        {
            _currentTime = _countdownTime;
            _isCountingDown = true;
            UpdateCountdownText();
        }
    }
}

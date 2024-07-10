using TMPro;
using UnityEngine;

namespace ShootingGallery
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private float countdownTime;
        [SerializeField] private TextMeshProUGUI countdownText;

        private float _currentTime;
        private bool _isCountingDown = false;

        private void Start()
        {
            _currentTime = countdownTime;
            GameStateManager.Instance.OnGameStart += StartCountdown;
            UpdateCountdownText();
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
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void StartCountdown()
        {
            _isCountingDown = true;
        }
    }
}

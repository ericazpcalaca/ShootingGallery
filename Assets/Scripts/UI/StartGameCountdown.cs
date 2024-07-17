using ShootingGallery;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShootingGallery
{
    public class StartGameCountdown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countdownText;

        private WaitForSeconds _waitOneSecond = new WaitForSeconds(1);

        private void Start()
        {
            GameStateManager.Instance.OnGameCountdown += HandleCountDown;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameCountdown -= HandleCountDown;
        }

        private void HandleCountDown()
        {
            StartCoroutine(Countdown(seconds: 3));
        }

        private IEnumerator Countdown(int seconds)
        {
            int count = seconds;

            while (count > 0)
            {
                _countdownText.text = count.ToString();
                yield return _waitOneSecond;
                count--;
            }
        }
    }
}

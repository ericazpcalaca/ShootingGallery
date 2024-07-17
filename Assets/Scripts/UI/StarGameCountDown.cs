using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShootingGallery
{
    public class StarGameCountDown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countDownText;
        void Start()
        {
            GameStateManager.Instance.OnGameCountDown += HandleCountDown;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameCountDown -= HandleCountDown;
        }
        private void HandleCountDown()
        {
            StartCoroutine(Countdown(3));
        }

        IEnumerator Countdown(int seconds)
        {
            int count = seconds;

            while (count > 0)
            {
                _countDownText.text = count.ToString();
                yield return new WaitForSeconds(1);
                count--;
            }
        }
        
    }
}
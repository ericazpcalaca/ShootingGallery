using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShootingGallery
{
    public class EndSessionButton : MonoBehaviour
    {
        [SerializeField] private Button _btnRetry;
        [SerializeField] private Button _btnExit;
        [SerializeField] private PlayableDirector _playableDirector;

        private CountdownTimer _countdownTimer;

        private void Start()
        {
            _btnRetry.onClick.AddListener(OnRetryButtonClick);
            _btnExit.onClick.AddListener(OnExitButtonClick);
            _countdownTimer = GetComponentInParent<CountdownTimer>();
        }

        private void OnRetryButtonClick()
        {
            _playableDirector.time = 0;
            _playableDirector.Play();
            _countdownTimer.RestartCountdown();
        }

        private void OnExitButtonClick()
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace ShootingGallery
{
    public class EndSessionButton : MonoBehaviour
    {
        [SerializeField] private Button _btnRetry;
        [SerializeField] private Button _btnExit;
        [SerializeField] private PlayableDirector _playableDirector;

        private void Start()
        {
            _btnRetry.onClick.AddListener(OnRetryButtonClick);
            _btnExit.onClick.AddListener(OnExitButtonClick);
        }

        private void OnRetryButtonClick()
        {
            _playableDirector.time = 0;
            _playableDirector.Play();
            GameStateManager.Instance.StartGame();
        }

        private void OnExitButtonClick()
        {
            Application.Quit();
        }
    }
}
using ShootingGallery;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootingGallery
{
    public class StartGameCountdown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtReady;
        [SerializeField] private TextMeshProUGUI _txtGo;

        private WaitForSeconds _waitOneSecond = new WaitForSeconds(1);
        private WaitForSeconds _waitTwoSeconds = new WaitForSeconds(2);

        private void Start()
        {
            _txtReady.gameObject.SetActive(false);
            _txtGo.gameObject.SetActive(false);

            GameStateManager.Instance.OnGameCountdown += HandleCountDown;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameCountdown -= HandleCountDown;
        }

        private void HandleCountDown()
        {
            _txtReady.gameObject.SetActive(false);
            _txtGo.gameObject.SetActive(false);
            StartCoroutine(ShowImagesSequence());
        }

        private IEnumerator ShowImagesSequence()
        {
            _txtReady.gameObject.SetActive(true);
            yield return _waitTwoSeconds;
            _txtReady.gameObject.SetActive(false);
            _txtGo.gameObject.SetActive(true) ;
            yield return _waitOneSecond;
            _txtGo.gameObject.SetActive(false);

        }

    }
}

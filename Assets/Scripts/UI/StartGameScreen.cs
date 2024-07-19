using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShootingGallery
{
    public class StartGameScreen : MonoBehaviour
    {
        [SerializeField] private Button _btnStart;
        [SerializeField] private Button _btnExit;
        void Start()
        {
            _btnStart.onClick.AddListener(OnStartGame);
            _btnExit.onClick.AddListener(OnExitGame);
        }

        private void OnStartGame()
        {
            SceneManager.LoadScene("MainScene");
        }

        private void OnExitGame()
        {
            Application.Quit();
        }

    }
}
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
        void Start()
        {
            _btnStart.onClick.AddListener(OnStartGame);
        }

        private void OnStartGame()
        {
            SceneManager.LoadScene("MainScene");
        }

    }
}
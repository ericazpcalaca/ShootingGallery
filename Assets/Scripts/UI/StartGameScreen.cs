using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShootingGallery
{
    public class StartGameScreen : MonoBehaviour
    {
        [SerializeField] private Button _btnStart;
        [SerializeField] private Button _btnInstruction;
        [SerializeField] private Button _btnExit;
        [SerializeField] private string _mainSceneName = "MainScene";

        void Start()
        {
            _btnStart.onClick.AddListener(OnStartGame);
            _btnInstruction.onClick.AddListener(OpenInstruction);
            _btnExit.onClick.AddListener(OnExitGame);
        }

        private void OnStartGame()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_mainSceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            Scene newScene = SceneManager.GetSceneByName(_mainSceneName);
            SceneManager.SetActiveScene(newScene);
        }

        private void OpenInstruction()
        {
        }

        private void OnExitGame()
        {
            Application.Quit();
        }

    }
}
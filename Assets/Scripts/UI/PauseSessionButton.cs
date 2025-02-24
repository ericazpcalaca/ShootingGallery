using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShootingGallery
{
    public class PauseSessionButton : MonoBehaviour
    {
        [SerializeField] private Button _btnPauseContinue;
        [SerializeField] private Button _btnPauseRetry;
        [SerializeField] private Button _btnPauseExit;
        [SerializeField] private PlayableDirector _gamePlayableDirector;
        [SerializeField] private PlayableDirector _targetPlayableDirector;

        private PlayerInput _playerInput;

        private void Start()
        {
            _btnPauseContinue.onClick.AddListener(OnPauseContinueButtonClick);
            _btnPauseRetry.onClick.AddListener(OnPauseRetryButtonClick);
            _btnPauseExit.onClick.AddListener(OnPauseExitButtonClick);       
            
            _playerInput = GetComponentInParent<PlayerInput>();
        }

        private void OnPauseContinueButtonClick()
        {
            GameStateManager.Instance.GamePause(false);
        }

        private void OnPauseRetryButtonClick()
        {
            GameStateManager.Instance.GameRestart();
        }

        private void OnPauseExitButtonClick()
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
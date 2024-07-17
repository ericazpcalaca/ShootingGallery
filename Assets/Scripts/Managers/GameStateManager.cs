using System;
using UnityEngine;
using UnityEngine.Playables;

namespace ShootingGallery
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        [SerializeField] PlayableDirector _targetPlayableDirector;
        [SerializeField] PlayableDirector _gamePlayableDirector;

        public Action OnGameStart;
        public Action OnGameEnd;
        public Action OnGameRestart;
        public Action OnGameCountDown;
        public Action<bool> OnGamePause;
        public bool HasGameEnded { get; private set; }
        public bool HasGamePaused { get; private set; }

        public void GameCountDown() {
            OnGameCountDown?.Invoke();
        }

        public void StartGame()
        {
            _targetPlayableDirector?.Play();
            OnGameStart?.Invoke();
            HasGameEnded = false;
            HasGamePaused = false;
        }

        public void EndGame()
        {
            _targetPlayableDirector?.Stop();
            OnGameEnd?.Invoke();
            HasGameEnded = true;
            HasGamePaused = false;
        }

        public void GamePause(bool isPaused)
        {
            OnGamePause?.Invoke(isPaused);
            if (isPaused)
            {
                _targetPlayableDirector.Pause();
                _gamePlayableDirector.Pause();
            }
            else
            {
                _targetPlayableDirector.Play();
                _gamePlayableDirector.Play();
            }
            HasGamePaused = isPaused;
        }

        public void GameRestart()
        {
            OnGameRestart?.Invoke();
            _targetPlayableDirector.Stop();
            _targetPlayableDirector.time = 0;

            _gamePlayableDirector.time = 0;
            _gamePlayableDirector.Play();
        }
    }
}
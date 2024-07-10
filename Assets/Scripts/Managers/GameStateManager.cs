using System;
using UnityEngine;
using UnityEngine.Playables;

namespace ShootingGallery
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        [SerializeField] PlayableDirector _playableDirector;

        public Action OnGameStart;
        public Action OnGameEnd;
        public bool HasGameEnded { get; private set; }

        public void StartGame()
        {
            _playableDirector?.Play();
            OnGameStart?.Invoke();
            HasGameEnded = false;
        }

        public void EndGame()
        {
            _playableDirector?.Stop();
            OnGameEnd?.Invoke();
            HasGameEnded = true;
        }
    }
}
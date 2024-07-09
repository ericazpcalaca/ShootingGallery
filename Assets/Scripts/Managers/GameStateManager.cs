using System;
using UnityEngine;

namespace ShootingGallery
{
    public class GameStateManager : MonoBehaviour
    {
        public static event Action OnGameStart;
        public static event Action OnGameEnd;

        public void StartGame()
        {
            Debug.Log("Game Start");
            OnGameStart?.Invoke();
        }

        public void EndGame()
        {
            Debug.Log("Game Ended");
            OnGameEnd?.Invoke();
        }
    }
}
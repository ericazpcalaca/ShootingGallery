using System;
using UnityEngine;

namespace ShootingGallery
{
    public class EndGameHandler : MonoBehaviour
    {
        public static event Action OnGameEnd;
        public void EndGame()
        {
            Debug.Log("Game Ended");
            OnGameEnd?.Invoke();
        }
    }
}
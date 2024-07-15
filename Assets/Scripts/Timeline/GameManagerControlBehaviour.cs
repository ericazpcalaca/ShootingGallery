using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace ShootingGallery
{
    /// <summary>
    /// Playable behaviour for targets on a timeline
    /// </summary>
    [Serializable]
    public class GameManagerControlBehaviour : PlayableBehaviour
    {
        public GameManagerClip ControlClip { get; set; }

        private bool _canRun;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            _canRun = true;
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (!Application.isPlaying || !_canRun)
            {
                return;
            }

            if (ControlClip == null)
            {
                Debug.LogError($"[{typeof(GameManagerControlBehaviour)}] - Control clip not set!");
                return;
            }

            if (ControlClip.Command == GameManagerClip.GameManagerCommand.StartGame)
            {
                GameStateManager.Instance.StartGame();
            }
            else if (ControlClip.Command == GameManagerClip.GameManagerCommand.EndGame)
            {
                GameStateManager.Instance.EndGame();
            }

            _canRun = false;
        }
    }
}
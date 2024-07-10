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

        /// <summary>
        /// This function is called when the Playable starts to play
        /// </summary>
        /// <param name="playable">The playable object</param>
        /// <param name="info">The frame data</param>
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (!Application.isPlaying)
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
        }
    }
}
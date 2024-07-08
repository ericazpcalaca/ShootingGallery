using System;
using UnityEngine;
using UnityEngine.Playables;

namespace ShootingGallery
{
    /// <summary>
    /// Playable behaviour for targets on a timeline
    /// </summary>
    [Serializable]
    public class TargetControlBehaviour : PlayableBehaviour
    {
        public TargetControlClip ControlClip { get; set; }

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
                Debug.LogError($"[{typeof(TargetControlBehaviour)}] - Control clip not set!");
                return;
            }

            TargetManager.Instance.Spawn(ControlClip.TargetPosition, ControlClip.TargetMovementType, ControlClip.MovementSpeed);
        }
    }
}

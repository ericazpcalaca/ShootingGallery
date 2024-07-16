using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ShootingGallery
{
    /// <summary>
    /// Clip asset for controlling the timeline.
    /// </summary>
    [System.Serializable]
    public class TargetControlClip : PlayableAsset, ITimelineClipAsset
    {
        public ClipCaps clipCaps { get { return ClipCaps.None; } }

        public Target.MovementType TargetMovementType;
        public float MovementSpeed = 2f;
        public uint Points;
        public Vector3 TargetPosition = Vector3.zero;

        private readonly TargetControlBehaviour _template = new(); //!< The template used when creating the clip

        private TargetControlBehaviour _clipBehaviour = new();

        /// <summary>
        /// Creates a new playable object
        /// </summary>
        /// <param name="graph">The playable graph</param>
        /// <param name="owner">The owner game object</param>
        /// <returns>A playable object</returns>
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<TargetControlBehaviour>.Create(graph, _template);
            _clipBehaviour = playable.GetBehaviour();
            _clipBehaviour.ControlClip = this;
            return playable;
        }
    }
}

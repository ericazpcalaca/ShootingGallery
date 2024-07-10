using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ShootingGallery
{
    /// <summary>
    /// Clip asset for controlling the timeline.
    /// </summary>
    [System.Serializable]
    public class GameManagerClip : PlayableAsset, ITimelineClipAsset
    {
        public enum GameManagerCommand
        {
            StartGame,
            EndGame,
        }

        public ClipCaps clipCaps { get { return ClipCaps.None; } }

        public GameManagerCommand Command;

        private readonly GameManagerControlBehaviour _template = new(); //!< The template used when creating the clip

        private GameManagerControlBehaviour _clipBehaviour = new();

        /// <summary>
        /// Creates a new playable object
        /// </summary>
        /// <param name="graph">The playable graph</param>
        /// <param name="owner">The owner game object</param>
        /// <returns>A playable object</returns>
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<GameManagerControlBehaviour>.Create(graph, _template);
            _clipBehaviour = playable.GetBehaviour();
            _clipBehaviour.ControlClip = this;
            return playable;
        }
    }
    
}
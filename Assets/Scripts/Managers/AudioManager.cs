using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGallery
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Audio Souce")]
        [SerializeField] private AudioSource _soundEffects;

        [Header("Audio Clip")]
        [SerializeField] private AudioClip _hitTarget;

        private void Awake()
        {
            if (_hitTarget == null)
            {
                Debug.LogError("Hit Target audio clip not assigned.");
            }
            _hitTarget.LoadAudioData();

            if (_soundEffects == null)
            {
                Debug.LogError("AudioSource component not found.");
            }
        }

        public void PlayHitTarget()
        {
            if (_hitTarget != null && _soundEffects != null)
            {
                _soundEffects.PlayOneShot(_hitTarget);
            }
            else
            {
                Debug.LogError("Cannot play hit target sound. AudioSource or AudioClip is missing.");
            }
        }
    }
}
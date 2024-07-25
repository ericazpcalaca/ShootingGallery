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
        [SerializeField] private AudioClip _clickButton;
        [SerializeField] private AudioClip _houverButton;
        [SerializeField] private AudioClip _ready;
        [SerializeField] private AudioClip _go;
        [SerializeField] private AudioClip _newHighScore;

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

        public void PlayClickButton()
        {
            if (_clickButton != null)
            {
                _soundEffects.PlayOneShot(_clickButton);
            }
        }

        public void PlayHouverButton()
        {
            if (_clickButton != null)
            {
                _soundEffects.PlayOneShot(_houverButton);
            }
        }

        public void PlayReady()
        {
            if (_clickButton != null)
            {
                _soundEffects.PlayOneShot(_ready);
            }
        }

        public void PlayGo()
        {
            if (_clickButton != null)
            {
                _soundEffects.PlayOneShot(_go);
            }
        }

        public void PlayNewHighScore()
        {
            if (_clickButton != null)
            {
                _soundEffects.PlayOneShot(_newHighScore);
            }
        }
    }
}
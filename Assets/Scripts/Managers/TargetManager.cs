using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShootingGallery
{
    public class TargetManager : Singleton<TargetManager>
    {
        [Serializable]
        public struct PointTier
        {
            public Texture2D texture;
            public int points;
        }

        [SerializeField] private uint _initPoolSize;
        [SerializeField] private GameObject _objectToPool;
        [SerializeField] private List<PointTier> _pointTiers;
        [SerializeField] private Texture2D _flashTexture;

        private Stack<Target> _targetPool;

        private void Start()
        {
            SetupPool();
        }

        public void Spawn(Vector3 worldPos, Target.MovementType movementType, float speed, uint points, int currentHit, int numOfHits)
        {
            var target = GetFromPool();
            if (target == null)
            {
                return;
            }

            target.transform.position = worldPos;
            target.CanMove = true;
            target.CurrentMovementType = movementType;
            target.Speed = speed;
            target.TargetScore = points;
            target.CurrentHit = currentHit;
            target.NumberOfHits = numOfHits;

            Texture2D targetTexture = GetTargetTexture(points);
            target.ConfigureMaterial(targetTexture);
        }

        public void ReturnToPool(Target target)
        {
            _targetPool.Push(target);
            target.gameObject.SetActive(false);
        }

        public void FlashStart(Target target)
        {
            Texture2D oldTexture = GetTargetTexture(target.TargetScore);
            target.ConfigureMaterial(_flashTexture);
            StartCoroutine(CallFlashStopAfterDelay(0.15f, target, oldTexture));
        }

        private IEnumerator CallFlashStopAfterDelay(float delay, Target target, Texture2D originalTexture)
        {
            yield return new WaitForSeconds(delay);
            FlashStop(target, originalTexture);
        }

        private void FlashStop(Target target, Texture2D originalTexture)
        {
            target.ConfigureMaterial(originalTexture);
        }
        private void SetupPool()
        {
            _targetPool = new Stack<Target>();
            for (int i = 0; i < _initPoolSize; i++)
            {
                GameObject targetGameObject = Instantiate(_objectToPool);
                Target target = targetGameObject.GetComponent<Target>();
                if (targetGameObject == null)
                {
                    Debug.LogError($"[{typeof(TargetManager).Name}] - Can't instantiate target from prefab!");
                    continue;
                }

                targetGameObject.SetActive(false);
                _targetPool.Push(target);
            }
        }

        private Target GetFromPool()
        {
            if (_targetPool.Count > 0)
            {
                Target target = _targetPool.Pop();
                target.gameObject.SetActive(true);
                return target;
            }
            else
            {
                GameObject targetGameObject = Instantiate(_objectToPool);
                return targetGameObject.GetComponent<Target>();
            }
        }

        private Texture2D GetTargetTexture(uint targetPoints)
        {
            foreach (var tier in _pointTiers)
            {
                if (targetPoints <= tier.points)
                {
                    return tier.texture;
                }
            }
            return _pointTiers[_pointTiers.Count - 1].texture; 
        }
    }
}
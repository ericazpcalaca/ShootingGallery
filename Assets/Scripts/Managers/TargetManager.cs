using System;
using System.Collections.Generic;
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

        //[SerializeField] private Texture2D _pointTierOneTexture;
        //[SerializeField] private Texture2D _pointTierTwoTexture;
        //[SerializeField] private Texture2D _pointTierThreTexture;
        //[SerializeField] private Texture2D _pointTierFourTexture;
        //[SerializeField] private int _pointTierOne = 10;
        //[SerializeField] private int _pointTierTwo = 50;
        //[SerializeField] private int _pointTierThree = 100;
        //[SerializeField] private int _pointTierFour = 150;

        private Stack<Target> _targetPool;

        private void Start()
        {
            SetupPool();
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
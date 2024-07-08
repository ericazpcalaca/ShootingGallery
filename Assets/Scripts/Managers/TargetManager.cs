using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ShootingGallery
{
    public class TargetManager : Singleton<TargetManager>
    {
        [SerializeField] private uint _initPoolSize;
        [SerializeField] private GameObject _objectToPool;

        // store the pooled objects in a collection
        private Stack<GameObject> _targetPool;

        // Start is called before the first frame update
        private void Start()
        {
            SetupPool();
        }

        private void SetupPool()
        {
            _targetPool = new Stack<GameObject>();
            for (int i = 0; i < _initPoolSize; i++)
            {
                GameObject _target = Instantiate(_objectToPool);
                _target.SetActive(false);
                _targetPool.Push(_target);

            }
        }

        public GameObject GetFromPool()
        {
            if( _targetPool.Count > 0)
            {
                GameObject _target = _targetPool.Pop();
                _target.SetActive(true);
                return _target;
            }
            else
            {
                GameObject _target = Instantiate(_objectToPool);
                return _target;
            }

        }

        public void ReturnToPool(GameObject target)
        {
            _targetPool.Push(target);
            target.SetActive(false);
        }
    }
}
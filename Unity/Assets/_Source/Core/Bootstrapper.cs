using System;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

namespace _Source.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int count;
        [SerializeField] private int speed;

        private Transform[] _objectOnScene;
        private TransformAccessArray _transformOnScene;
        private MovementJob _movementJob;
        private JobHandle _movementJobHandle;

        void Awake()
        {
            _objectOnScene = new Transform[count];
            
            for (int i = 0; i < count; i++)
            {
                Vector3 position =
                    new Vector3(Random.Range(-75, 75), 0, Random.Range(-75, 75));
                GameObject instance = Instantiate(prefab, position, Quaternion.identity);
                _objectOnScene[i] = instance.transform;
            }

            _transformOnScene = new TransformAccessArray(_objectOnScene);
        }

        private void Update()
        {
            _movementJob = new MovementJob()
            {
                Speed = speed
            };
            _movementJobHandle = _movementJob.Schedule(_transformOnScene);
        }

        private void LateUpdate()
        {
            _movementJobHandle.Complete();
        }

        private void OnDisable()
        {
            _transformOnScene.Dispose();
        }
    }
}
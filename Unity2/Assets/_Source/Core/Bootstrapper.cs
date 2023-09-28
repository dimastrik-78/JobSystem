using System.Collections;
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
        [SerializeField] private float speed;
        [SerializeField] private int time;

        private Transform[] _objectOnScene;
        private TransformAccessArray _transformOnScene;
        private MovementJob _movementJob;
        private JobHandle _movementJobHandle;
        private CalculationJob _calculationJob;

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
            StartCoroutine(Timer());
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

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(time);
            _calculationJob = new CalculationJob()
            {
                Num = Random.Range(1, 100)
            };
            JobHandle handle = _calculationJob.Schedule();
            handle.Complete();
            StartCoroutine(Timer());
        }
    }
}
using UnityEngine;
using UnityEngine.Jobs;

namespace _Source.Core
{
    public struct MovementJob : IJobParallelForTransform
    {
        public int Speed;
    
        public void Execute(int index, TransformAccess transform)
        {
            transform.position += Vector3.forward * Speed;
        }
    }
}
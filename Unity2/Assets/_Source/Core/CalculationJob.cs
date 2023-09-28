using System;
using Unity.Jobs;
using UnityEngine;

namespace _Source.Core
{
    public struct CalculationJob : IJob
    {
        public int Num;
        
        public void Execute()
        {
            Debug.Log(Log());
        }

        private double Log()
        {
            return Math.Log(Num);
        }
    }
}
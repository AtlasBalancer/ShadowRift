using System;
using com.ab.core;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    [Serializable]
    public struct Timer : IComponent
    {
        public float Delay;
        public float Max;

        public Timer(float max, bool beginningStart = false)
        {
            Max = max;
            Delay = beginningStart ? max : 0f;
        }
        
        public bool Next(float delta, bool repeat = true)
        {
            Delay += delta;

            if (Delay >= Max)
            {
                Delay = repeat ? Max - Delay : 0;
                return true;
            }

            return false;
        }
    }

    public static class TimerExtensions
    {
        public static bool Timer(this World<WT>.Entity source, float deltaTime)
        {
            ref var timer = ref source.Ref<Timer>();
            return !timer.Next(deltaTime);
        }

        public static World<WT>.Entity SetTimer(this World<WT>.Entity source, Vector2 rangeDelay, bool beginningStart = false)
        {
            source.Set(new Timer(rangeDelay.Rand(), beginningStart));
            return source;
        }
        
        public static World<WT>.Entity SetTimer(this World<WT>.Entity source, float delay, bool beginningStart = false)
        {
            source.Set(new Timer(delay, beginningStart));
            return source;
        }
    }
}
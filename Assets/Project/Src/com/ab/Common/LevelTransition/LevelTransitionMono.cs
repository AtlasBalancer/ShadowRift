using System;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common.LevelTransition
{

    public readonly struct LevelTransitionRef : IComponent
    {
        public readonly LevelTransitionMono Val;
        public LevelTransitionRef(LevelTransitionMono val) => Val = val;
    }
    
    public class LevelTransitionMono : EntityLink
    {
        public string LevelName;
        
        protected override void Register()
        {
            Ent.Add(new LevelTransitionRef(this));
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Ent.ApplyTag<LevelTransitionTag>(true);
        }
    }
}
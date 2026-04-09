using com.ab.common;
using com.ab.common.ProgressBar;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.domain.harv
{
    public readonly struct HarvRef : IComponent
    {
        public readonly HarvMono Val;

        public HarvRef(HarvMono val) => Val = val;
    }
    
    public class HarvMono : EntityLink
    {
        public ProgressBarMono ProgressBar;
        public SpriteRenderer SR;

        protected override void Register() => 
            Ent.Set(new HarvRef(this));

        public void SetSprite(Sprite sprite) => 
            SR.sprite = sprite;
    }
}
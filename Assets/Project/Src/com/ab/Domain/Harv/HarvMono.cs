using com.ab.common;
using UnityEngine;

namespace com.ab.domain.harv
{
    public class HarvMono : EntityLink
    {
        public SpriteRenderer SR;

        public void SetSprite(Sprite sprite) => 
            SR.sprite = sprite;
    }
}
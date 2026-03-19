using com.ab.common;
using UnityEngine;

namespace Project.Src.com.ab.Domain.Harvest
{
    public class HarvMono : EntityLink
    {
        public SpriteRenderer SR;

        public void SetSprite(Sprite sprite) => 
            SR.sprite = sprite;
    }
}
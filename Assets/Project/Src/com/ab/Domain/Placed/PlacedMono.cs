using com.ab.common;
using UnityEngine;
using DG.Tweening;
using FFS.Libraries.StaticEcs;
using Sequence = DG.Tweening.Sequence;

namespace Project.Src.com.ab.Domain.Collect
{
    public class PlacedMono : EntityLink
    {
        public SpriteRenderer Render;

        protected override void RegisterComponentRef()
        {
            Ent.Add(new PlacedRef(this));
        }

        public void UpdateRender(Sprite sprite) => Render.sprite = sprite;
        
        public void FlyTo(Vector3 target, float duration)
        {
            Debug.Log($"{nameof(PlacedMono)}::from: {transform.position}; to: {target}, dur: {duration}");
            
            transform.DOMove(target, duration).SetEase(Ease.InOutQuad);
        }

        public void Drop(Vector3 startPos)
        {
            transform.position = startPos;
        
            Vector2 randomOffset = Random.insideUnitCircle * 1.2f;
            Vector3 endPos = startPos + new Vector3(randomOffset.x, randomOffset.y, 0);
        
            float height = 1.5f;
            float duration = 0.5f;
        
            Sequence seq = DOTween.Sequence();
        
            seq.Append(
                transform.DOMove(
                    new Vector3(endPos.x, startPos.y + height, 0),
                    duration * 0.5f
                ).SetEase(Ease.OutQuad)
            );
        
            seq.Append(
                transform.DOMove(endPos, duration * 0.5f)
                    .SetEase(Ease.InQuad)
            );
        }
    }

    public readonly struct PlacedRef : IComponent
    {
        public readonly PlacedMono Ref;
        public PlacedRef(PlacedMono @ref) => Ref = @ref;
    }
}
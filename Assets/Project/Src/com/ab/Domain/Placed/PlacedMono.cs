using System;
using com.ab.common;
using com.ab.complexity.core;
using UnityEngine;
using DG.Tweening;
using FFS.Libraries.StaticEcs;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

namespace Project.Src.com.ab.Domain.Collect
{
    public class PlacedMono : EntityLink
    {
        public SpriteRenderer Render;
        public Collider2D Collider;
        public Sequence _tweenSeq;

        protected override void RegisterComponentRef()
        {
            Ent.Add(new PlacedRef(this));
        }

        public void UpdateRender(Sprite sprite) => Render.sprite = sprite;

        public void PickUp(Vector3 target, float duration, bool disableCollider = true)
        {
            Collider.enabled = false;


            _tweenSeq.Kill();
            _tweenSeq = DOTween.Sequence(
                transform.DOMove(target, duration).SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        this.Active(false);
                        Ent.Destr();
                        Debug.Log("Complete");
                    }));
        }

        public void Drop(Vector3 start)
        {
            transform.position = start;

            Vector2 randomOffset = Random.insideUnitCircle * 1.2f;
            Vector3 endPos = start + new Vector3(randomOffset.x, randomOffset.y, 0);

            float height = 1.5f;
            float duration = 0.5f;

            var seq = DOTween.Sequence()
                .Append(transform
                    .DOMove(new Vector3(endPos.x, start.y + height, 0), duration * 0.5f)
                    .SetEase(Ease.OutQuad))
                .Append(transform.DOMove(endPos, duration * 0.5f)
                    .SetEase(Ease.InQuad));
            
            _tweenSeq.Kill();
            _tweenSeq = seq;
        }

        void OnDestroy()
        {
            _tweenSeq.Kill();
            _tweenSeq = null;
        }
    }

    public readonly struct PlacedRef : IComponent
    {
        public readonly PlacedMono Ref;
        public PlacedRef(PlacedMono @ref) => Ref = @ref;
    }
    
}
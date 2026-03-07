using UnityEngine;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;
using Project.Src.com.ab.Complexity.Features.Topdown;

namespace Project.Src.com.ab.Domain.Collect
{
    public class CollectMono : ResourceMono
    {
        public int Amount;
        
        public void FlyTo(Vector3 target, float duration)
        {
            transform.DOMove(target, duration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    // Добавляем ресурс
                    // Despawn из пула
                });
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

        public void OnReuse()
        {
            gameObject.SetActive(true);
        }

        public void OnRelease()
        {
            gameObject.SetActive(false);
        }
    }
}
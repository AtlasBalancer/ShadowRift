using com.ab.common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.complexity
{
    public class MovementSamePositionMono : EntityLink
    {
        public bool Self;

        [HideIf(nameof(Self))] public Transform UpdateSource;

        public Transform TargetSource;

        void OnValidate()
        {
            if (Self) UpdateSource = transform;
        }

        protected override void Register()
        {
            Ent.Set(ToComponent());
            base.Register();
        }

        public MovementSamePosition ToComponent()
        {
            return new MovementSamePosition
            {
                UpdateSource = UpdateSource,
                TargetSource = TargetSource
            };
        }

        public void UpdateTarget(Transform source)
        {
            ref var item = ref Ent.Ref<MovementSamePosition>();
            item.TargetSource = source;
        }
    }
}
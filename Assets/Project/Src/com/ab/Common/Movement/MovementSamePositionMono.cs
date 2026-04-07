using UnityEngine;
using com.ab.common;
using Sirenix.OdinInspector;

namespace com.ab.complexity
{
    public class MovementSamePositionMono : EntityLink
    {
        public bool Self;
        
        [HideIf(nameof(Self))]
        public Transform UpdateSource;
        public Transform TargetSource;

        protected override void Register()
        {
            Ent.Add(this.ToComponent());
            base.Register();
        }

        public MovementSamePosition ToComponent() =>
            new()
            {
                UpdateSource = this.UpdateSource,
                TargetSource = this.TargetSource
            };

        void OnValidate()
        {
            if (Self) UpdateSource = this.transform;
        }

        public void UpdateTarget(Transform source)
        {
            ref var item = ref Ent.Ref<MovementSamePosition>();
            item.TargetSource = source;
        }
    }
}
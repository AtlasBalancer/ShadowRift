using System;
using UnityEngine;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Sirenix.OdinInspector;

namespace com.ab.common.Camera
{
    public struct CameraZoomSystem : ISystem
    {
        readonly Settings _def;
        const float TOLERANCE = 0.01f;

        [Serializable]
        public class Settings
        {
            public float Speed;
        }

        public CameraZoomSystem(Settings def) => _def = def;

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            foreach (var ent in W.Query<All<CameraZoom>>().Entities())
            {
                ref var item = ref ent.Ref<CameraZoom>();

                if (Math.Abs(item.From - item.To) < TOLERANCE)
                    continue;

                var camera = ent.Ref<CameraRef>().Val.Camera;

                float move = Mathf.MoveTowards(item.From, item.To, _def.Speed * deltaTime);

                camera.orthographicSize = move;
                item.From = move;
            }
        }
    }

    public struct CameraZoom : IComponent
    {
        public float From;
        public float To;
    }

    public class CameraZoomMono : EntityLink
    {
        public float To;

        protected override void Register()
        {
            Ent.Set(ToComponent());
            base.Register();
        }

        public CameraZoom ToComponent()
        {
            return new()
            {
                From = this.To,
                To = this.To
            };
        }

        [Button]
        public void ChangeZoom()
        {
            ref var item = ref Ent.Ref<CameraZoom>();
            item.To = this.To;
        }

        public void UpdateZoom(float to)
        {
            To = to;
            ChangeZoom();
        }
    }
}
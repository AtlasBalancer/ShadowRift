using System;
using com.ab.common.Camera;
using com.ab.complexity;
using com.ab.complexity.core;
using com.ab.core;
using FFS.Libraries.StaticEcs;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace com.ab.domain.construct
{
    public class ConstructViewSystem : ViewPresenter<ConstructView>, IInitSystem, IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public float ZoomSize;
            public Button Btn;
            public ConstructView ViewRef;
            public Transform MovementPoint;
            public RectTransform[] BeforeShowNeedToHide;
            public MovementSamePositionMono MainCameraMover;
            public CameraZoomMono MainCameraZoom;

            public ConstructionMono[] StaticConstructions;
        }

        readonly Settings _def;
        Transform _activeTargetCache;
        float _zoomFromCache;

        public ConstructViewSystem(Settings def)
        {
            _def = def;
            Register(_def.ViewRef, _def.Btn);
        }

        public void Init()
        {
            _def.StaticConstructions.ForEach(item => item.Init(true));
        }

        protected override void Show()
        {
            ActiveItems(false);
            ActiveCamera(true);
            ActiveConstructionsUi(true);
            base.Show();
        }

        protected override void Hide()
        {
            ActiveItems(true);
            ActiveCamera(false);
            ActiveConstructionsUi(false);
            base.Hide();
        }

        void ActiveCamera(bool active)
        {
            if (active)
            {
                _activeTargetCache = _def.MainCameraMover.Ent.Ref<MovementSamePosition>().TargetSource;
                _def.MovementPoint.transform.position = _activeTargetCache.position;
                _def.MainCameraMover.UpdateTarget(_def.MovementPoint);

                _zoomFromCache = _def.MainCameraZoom.Ent.Ref<CameraZoom>().From;
                _def.MainCameraZoom.UpdateZoom(_def.ZoomSize);
            }
            else
            {
                _def.MainCameraMover.UpdateTarget(_activeTargetCache);
                _def.MainCameraZoom.UpdateZoom(_zoomFromCache);
            }
        }

        void ActiveConstructionsUi(bool active)
        {
            foreach (var item in _def.StaticConstructions)
            {
               if(item.Ent.HasAllOfTags<ConstructionBuilt>())
                   continue;

               item.ActiveUi(active);
            }
        }

        void ActiveItems(bool active)
        {
            var items = _def.BeforeShowNeedToHide;

            for (var i = 0; i < items.Length; i++)
                items[i].Active(active);
        }

        public void Update() { }
    }
}
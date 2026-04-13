using System;
using com.ab.core;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.ab.domain.camera
{
    /// <summary>
    /// RTS-управление для любого Transform:
    ///   — 1 палец / ПКМ (editor) → панорамирование
    ///   — 2 пальца / колесо (editor) → масштаб (только если Camera != null)
    /// </summary>
    public class CameraRtsSystem : ISystem
    {
        readonly Settings _settings;
        float _prevPinchDistance;

        public CameraRtsSystem(Settings settings) => _settings = settings;

        public void Update()
        {
            var service = W.GetResource<CameraService>();

#if UNITY_EDITOR
            UpdateEditor(service);
#else
            UpdateTouch(service);
#endif
        }

        // ──────────────────────────────────────────────────────────────────── touch

        void UpdateTouch(CameraService service)
        {
            if (Input.touchCount == 1)
                HandlePan(service);
            else if (Input.touchCount == 2)
                HandlePinchZoom(service);
            else
                _prevPinchDistance = 0f;
        }

        void HandlePan(CameraService service)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Moved) return;
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;

            ApplyPanDelta(service, touch.deltaPosition);
        }

        void HandlePinchZoom(CameraService service)
        {
            var t0 = Input.GetTouch(0);
            var t1 = Input.GetTouch(1);

            if (t0.phase == TouchPhase.Began || t1.phase == TouchPhase.Began)
            {
                _prevPinchDistance = Vector2.Distance(t0.position, t1.position);
                return;
            }

            var currentDist = Vector2.Distance(t0.position, t1.position);
            if (_prevPinchDistance > 0f)
                ApplyZoomDelta(service, -(currentDist - _prevPinchDistance) * _settings.ZoomSpeed);

            _prevPinchDistance = currentDist;
        }

        // ──────────────────────────────────────────────────────────────────── editor

#if UNITY_EDITOR
        void UpdateEditor(CameraService service)
        {
            if (Input.GetMouseButton(1))
            {
                var mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                // преобразуем скорость мыши (в пикселях/сек × Time.deltaTime) в экранные пиксели
                var screenDelta = mouseDelta / (service.ViewSize * 2f / Screen.height) * Time.deltaTime * 60f;
                ApplyPanDelta(service, screenDelta);
            }

            var scroll = Input.mouseScrollDelta.y;
            if (Mathf.Abs(scroll) > 0.001f)
                ApplyZoomDelta(service, -scroll * _settings.EditorScrollZoomSpeed);
        }
#endif

        // ──────────────────────────────────────────────────────────────────── core

        void ApplyPanDelta(CameraService service, Vector2 screenDelta)
        {
            // screenDelta → world: 1px = (viewSize * 2) / screenHeight мировых единиц
            var scale = service.ViewSize * 2f / Screen.height;
            var worldDelta = new Vector3(-screenDelta.x * scale, -screenDelta.y * scale, 0f);
            service.Target.position = ClampPosition(service.Target.position + worldDelta, service);
        }

        void ApplyZoomDelta(CameraService service, float delta)
        {
            var newSize = Mathf.Clamp(service.ViewSize + delta, _settings.MinZoom, _settings.MaxZoom);
            service.ViewSize = newSize;

            if (service.Camera != null)
                service.Camera.orthographicSize = newSize;

            service.Target.position = ClampPosition(service.Target.position, service);
        }

        Vector3 ClampPosition(Vector3 pos, CameraService service)
        {
            if (!_settings.UseBounds) return pos;
            var halfH = service.ViewSize;
            var halfW = service.ViewSize * GetAspect(service);
            pos.x = Mathf.Clamp(pos.x, _settings.Bounds.xMin + halfW, _settings.Bounds.xMax - halfW);
            pos.y = Mathf.Clamp(pos.y, _settings.Bounds.yMin + halfH, _settings.Bounds.yMax - halfH);
            return pos;
        }

        static float GetAspect(CameraService service) =>
            service.Camera != null ? service.Camera.aspect : (float)Screen.width / Screen.height;

        // ──────────────────────────────────────────────────────────────────── settings

        [Serializable]
        public class Settings
        {
            public float MinZoom = 3f;
            public float MaxZoom = 15f;
            public float ZoomSpeed = 0.05f;
            public float EditorScrollZoomSpeed = 0.5f;
            public bool UseBounds;
            public Rect Bounds;
        }
    }
}

using UnityEngine;

namespace com.ab.domain.camera
{
    /// <summary>
    ///     Контекст RTS-движения. Target — любой Transform (камера, фон, карта и т.д.).
    ///     Camera заполняется только если нужен pinch-zoom через orthographicSize.
    /// </summary>
    public class CameraService
    {
        public Camera Camera; // optional — для zoom через orthographicSize
        public Transform Target;
        public float ViewSize; // текущий "размер вида"; зеркалит Camera.orthographicSize если Camera != null
    }
}
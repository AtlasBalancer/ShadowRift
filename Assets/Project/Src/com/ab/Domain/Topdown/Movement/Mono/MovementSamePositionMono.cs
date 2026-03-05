using com.ab.core;
using UnityEngine;

namespace com.ab.complexity
{
    public class MovementSamePositionMono : MonoEntity
    {
        public Transform UpdateSource;
        public Transform PositionSource;

        public MovementSamePosition ToComponent() =>
            new()
            {
                UpdateSource = this.UpdateSource,
                PositionSource = this.PositionSource
            };
    }
}
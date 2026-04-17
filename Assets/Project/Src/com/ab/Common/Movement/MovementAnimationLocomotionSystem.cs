using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    public readonly struct MovementAnimationLocomotionSystem : ISystem
    {
        public static readonly int VELOCITY_KEY = Animator.StringToHash("Velocity");

        public void Update()
        {
            foreach (var ent in W.Query<All<Velocity, AnimatorRef>>().Entities())
            {
                ref var anim = ref ent.Ref<AnimatorRef>();
                ref var vel = ref ent.Ref<Velocity>();

                anim.Value.SetFloat(VELOCITY_KEY, vel.Magnitude);
            }
        }
    }
}
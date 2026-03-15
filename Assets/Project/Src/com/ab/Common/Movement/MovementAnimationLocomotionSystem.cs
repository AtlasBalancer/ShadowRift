using FFS.Libraries.StaticEcs;

namespace com.ab.complexity.core
{
    public readonly struct MovementAnimationLocomotionSystem : IUpdateSystem
    {
        public void Update()
        {
            foreach (var ent in W.Query.Entities<All<Velocity, AnimatorRef>>())
            {
                ref var anim = ref ent.Ref<AnimatorRef>();
                ref var vel = ref ent.Ref<Velocity>();

                anim.Value.SetFloat(Const.VELOCITY_KEY, vel.Magnitude);
            }
        }
    }
}
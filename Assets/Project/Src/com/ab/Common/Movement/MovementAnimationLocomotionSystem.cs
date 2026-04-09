using FFS.Libraries.StaticEcs;

namespace com.ab.complexity.core
{
    public readonly struct MovementAnimationLocomotionSystem : ISystem
    {
        public void Update()
        {
            foreach (var ent in W.Query<All<Velocity, AnimatorRef>>().Entities())
            {
                ref var anim = ref ent.Ref<AnimatorRef>();
                ref var vel = ref ent.Ref<Velocity>();

                anim.Value.SetFloat(Const.VELOCITY_KEY, vel.Magnitude);
            }
        }
    }
}
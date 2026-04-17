using com.ab.common;
using com.ab.complexity.player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ab.complexity.features.player
{
    public class PlayerMono : EntityLink
    {
        public Animator Animator;

        [Button]
        public void SetTool()
        {
            if (Ent.Has<Tool>())
                Ent.Delete<Tool>();
            else
                Ent.Add<Tool>();
        }

        protected override void Register()
        {
            Ent.Set(new PlayerRef { Ref = this });
            Ent.Set<PlayerTag>();
        }
    }
}
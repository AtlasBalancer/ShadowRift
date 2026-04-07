using com.ab.common;
using com.ab.complexity.player;
using UnityEngine;
using Sirenix.OdinInspector;

namespace com.ab.complexity.features.player
{
    public class PlayerMono : EntityLink
    {
        public Animator Animator;

        [Button]
        public void SetTool()
        {
            if (Ent.HasAllOf<Tool>())
                Ent.Delete<Tool>();
            else
                Ent.Add<Tool>();
        }

        protected override void Register()
        {
            Ent.Add(new PlayerRef { Ref = this });
            Ent.SetTag<PlayerTag>();
        }
    }
}
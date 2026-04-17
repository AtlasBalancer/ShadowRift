using com.ab.common;
using com.ab.complexity.features.player;
using UnityEngine;

namespace Project.Src.com.ab.Complexity.Core.Static.Mono
{
    public class LogicRendererMono : EntityLink
    {
        public Transform Renderer;

        protected override void Register()
        {
            Ent.Set(new LogicRender(Renderer));
        }
    }
}
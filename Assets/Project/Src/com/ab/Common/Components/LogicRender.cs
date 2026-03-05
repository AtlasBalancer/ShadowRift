using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.complexity.features.player
{
    public readonly struct LogicRender : IComponent
    {
       public readonly Transform Value;

        public LogicRender(Transform renderer) => 
            Value = renderer;
    }
}
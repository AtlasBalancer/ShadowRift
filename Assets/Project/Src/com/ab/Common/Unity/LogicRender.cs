using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    public readonly struct LogicRender : IComponent
    {
        public readonly Transform Value;

        public LogicRender(Transform renderer)
        {
            Value = renderer;
        }
    }
}
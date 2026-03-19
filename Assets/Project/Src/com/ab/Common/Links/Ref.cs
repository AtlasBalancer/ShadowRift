using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace com.ab.common
{
    public readonly struct Ref : IComponent
    {
        public readonly Transform Val;
        public Ref(Transform val) => Val = val;
    }
}
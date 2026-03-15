using UnityEngine;
using FFS.Libraries.StaticEcs;

namespace com.ab.complexity.core
{
    public struct Ref : IComponent
    {
        public Transform Value;
    }
}
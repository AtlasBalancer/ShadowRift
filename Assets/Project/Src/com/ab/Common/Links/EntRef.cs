using System;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public readonly struct EntRef : IComponent, IEquatable<EntRef>
    {
        public readonly W.Entity ID;

        public EntRef(W.Entity id) =>
            ID = id;

        public bool Equals(EntRef other) => ID == other.ID;

        public override bool Equals(object obj) => obj is ConfigRef other && Equals(other);

        public override int GetHashCode() => ID.GetHashCode();
        
        public static bool operator ==(EntRef a, EntRef b) => a.ID == b.ID;
        public static bool operator !=(EntRef a, EntRef b) => a.ID != b.ID;
    }
}
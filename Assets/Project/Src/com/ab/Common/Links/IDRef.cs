using System;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public readonly struct IDRef : IComponent, IEquatable<IDRef>
    {
        public readonly IDEntSo ID;

        public IDRef(IDEntSo id) =>
            ID = id;

        public bool Equals(IDRef other) => ID == other.ID;

        public override bool Equals(object obj) => obj is IDRef other && Equals(other);

        public override int GetHashCode() => ID.GetHashCode();
        
        public static bool operator ==(IDRef a, IDRef b) => a.ID == b.ID;
        public static bool operator !=(IDRef a, IDRef b) => a.ID != b.ID;
    }
}
using System;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public readonly struct EntRef : IComponent, IEquatable<EntRef>
    {
        public readonly World<WT>.Entity ID;

        public EntRef(World<WT>.Entity id)
        {
            ID = id;
        }

        public bool Equals(EntRef other)
        {
            return ID == other.ID;
        }

        public override bool Equals(object obj)
        {
            return obj is ConfigRef other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(EntRef a, EntRef b)
        {
            return a.ID == b.ID;
        }

        public static bool operator !=(EntRef a, EntRef b)
        {
            return a.ID != b.ID;
        }
    }
}
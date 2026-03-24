using System;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public readonly struct ConfigRef : IComponent, IEquatable<ConfigRef>
    {
        public readonly uint Id; // raw WC entity index

        public ConfigRef(uint id) => Id = id;

        public ConfigRef(WC.Entity wcEnt) => Id = wcEnt.Gid().Id;

        public WC.Entity Unpack() => WC.Entity.FromIdx(Id);

        public bool Equals(ConfigRef other) => Id == other.Id;
        public static bool operator ==(ConfigRef a, ConfigRef b) => a.Id == b.Id;
        public static bool operator !=(ConfigRef a, ConfigRef b) => a.Id != b.Id;
        public override int GetHashCode() => (int)Id;
    }
}
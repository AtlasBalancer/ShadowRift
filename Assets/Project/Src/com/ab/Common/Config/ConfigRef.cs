using System;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public readonly struct ConfigRef : IComponent, IEquatable<ConfigRef>
    {
        public readonly string PersistenId;
        public readonly EntityGID Gid;

        public ConfigRef(EntityGID gid, string persistenId)
        {
            Gid = gid;
            PersistenId = persistenId;
        }

        public ConfigRef(WC.Entity wcEnt, string persistenId)
        {
            Gid = wcEnt.GID;
            PersistenId = persistenId;
        }

        public WC.Entity Unpack() => Gid.Unpack<WCT>();

        public bool Equals(ConfigRef other) => Gid == other.Gid;
        public static bool operator ==(ConfigRef a, ConfigRef b) => a.Gid == b.Gid;
        public static bool operator !=(ConfigRef a, ConfigRef b) => a.Gid != b.Gid;
        public override int GetHashCode() => Gid.GetHashCode();
    }
}
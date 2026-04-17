using System;
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

        public ConfigRef(World<WCT>.Entity wcEnt, string persistenId)
        {
            Gid = wcEnt.GID;
            PersistenId = persistenId;
        }

        public World<WCT>.Entity Unpack()
        {
            return Gid.Unpack<WCT>();
        }

        public bool Equals(ConfigRef other)
        {
            return Gid == other.Gid;
        }

        public static bool operator ==(ConfigRef a, ConfigRef b)
        {
            return a.Gid == b.Gid;
        }

        public static bool operator !=(ConfigRef a, ConfigRef b)
        {
            return a.Gid != b.Gid;
        }

        public override int GetHashCode()
        {
            return Gid.GetHashCode();
        }
    }
}
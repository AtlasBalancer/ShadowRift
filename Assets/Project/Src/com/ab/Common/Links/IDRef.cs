using System;
using com.ab.complexity.core;
using com.ab.domain.item;
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

    public static class IDRefExtensions
    {
        public static bool TryToFindIDRefByTag<TTag>(this IDRef source, out W.Entity findingEnt)
            where TTag : struct, ITag
        {
            foreach (var ent in W.Query.Entities<All<IDRef>, TagAll<TTag>>())
            {
                if (ent.Ref<IDRef>().ID.RuntimeID == source.ID.RuntimeID)
                {
                    findingEnt = ent;
                    return true;
                }
            }

            findingEnt = default;
            return false;
        }

        public static ItemEntry GetItemDef(this W.Entity source) => 
            source.Ref<IDRef>().GetItemDef();
        
        public static ItemEntry GetItemDef(this IDRef source) => 
            source.ID.RuntimeID.Ref<ItemEntry>();
    }
}
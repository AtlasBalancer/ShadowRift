using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public static class ConfigExtensions
    {
        public static bool TryToFindConfigRefByTag<TTag>(this ConfigIDEntSo source, out W.Entity findingEnt,
            out uint idRef)
            where TTag : struct, ITag
        {
            var @ref = source.RuntimeID.Ref<ConfigRef>();
            idRef = @ref.Id;

            return TryToFindConfigRefByTag<TTag>(@ref, out findingEnt);
        }
        
        public static bool TryToFindConfigRefByTag<TTag>(this W.Entity source, out W.Entity findingEnt, out uint idRef)
            where TTag : struct, ITag
        {
            var @ref = source.Ref<ConfigRef>();
            idRef = @ref.Id;
            
            return TryToFindConfigRefByTag<TTag>(@ref, out findingEnt);
        }

        public static bool TryToFindConfigRefByTag<TTag>(this ConfigRef source, out W.Entity findingEnt)
            where TTag : struct, ITag
        {
            foreach (var ent in W.Query.Entities<All<ConfigRef>, TagAll<TTag>>())
            {
                if (ent.Ref<ConfigRef>().Equals(source))
                {
                    findingEnt = ent;
                    return true;
                }
            }

            findingEnt = default;
            return false;
        }
 
        public static bool TryToFindConfigRef<TComponent>(this ConfigIDEntSo source, out W.Entity findingEnt,
            out uint idRef)
            where TComponent : struct, IComponent
        {
            var @ref = source.RuntimeID.Ref<ConfigRef>();
            idRef = @ref.Id;

            return TryToFindConfigRef<TComponent>(@ref, out findingEnt);
        }
        
        public static bool TryToFindConfigRef<TComponent>(this ConfigRef source, out W.Entity findingEnt)
            where TComponent : struct, IComponent
        {
            foreach (var ent in W.Query.Entities<All<ConfigRef, TComponent>>())
            {
                if (ent.Ref<ConfigRef>().Equals(source))
                {
                    findingEnt = ent;
                    return true;
                }
            }

            findingEnt = default;
            return false;
        }

        public static TConfigTable GetConfigTable<TConfigTable>(this W.Entity source)
            where TConfigTable : struct, IComponent =>
            source.Ref<ConfigRef>().GetConfigTable<TConfigTable>();

        public static TConfigTable GetConfigTable<TConfigTable>(this ConfigRef source)
            where TConfigTable : struct, IComponent =>
            WC.Entity.FromIdx(source.Id).Ref<TConfigTable>();
    }
}
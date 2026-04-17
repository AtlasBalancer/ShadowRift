using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public static class ConfigExtensions
    {
        public static bool GetConfig<TComponent>(this ConfigIDEntSo source, out TComponent component,
            out ConfigRef @ref)
            where TComponent : struct, IComponent
        {
            @ref = source.RuntimeID.Ref<ConfigRef>();
            return @ref.GetConfig(out component);
        }

        public static bool GetConfig<TComponent>(this ConfigRef source, out TComponent component)
            where TComponent : struct, IComponent
        {
            foreach (var entC in WC.Query<All<ConfigRef, TComponent>>().Entities())
                if (entC.Ref<ConfigRef>().Equals(source))
                {
                    component = entC.Ref<TComponent>();
                    return true;
                }

            component = default;
            return false;
        }


        public static bool TryToFindRuntimeRefByTag<TTag>(this ConfigIDEntSo source, out World<WT>.Entity findingEnt,
            out EntityGID gid)
            where TTag : struct, ITag
        {
            var @ref = source.RuntimeID.Ref<ConfigRef>();
            gid = @ref.Gid;

            return TryToFindRuntimeRefByTag<TTag>(@ref, out findingEnt);
        }

        public static bool TryToFindRuntimeRefByTag<TTag>(this World<WT>.Entity source, out World<WT>.Entity findingEnt,
            out ConfigRef config)
            where TTag : struct, ITag
        {
            config = source.Ref<ConfigRef>();
            return TryToFindRuntimeRefByTag<TTag>(config, out findingEnt);
        }


        public static bool TryToFindRuntimeRefByTag<TTag>(this ConfigRef source, out World<WT>.Entity findingEnt)
            where TTag : struct, ITag
        {
            foreach (var ent in W.Query<All<ConfigRef, TTag>>().Entities())
                if (ent.Ref<ConfigRef>().Equals(source))
                {
                    findingEnt = ent;
                    return true;
                }

            findingEnt = default;
            return false;
        }

        public static bool TryToFindRuntimeRef<TComponent>(this ConfigIDEntSo source, out World<WT>.Entity findingEnt,
            out EntityGID gid)
            where TComponent : struct, IComponent
        {
            var @ref = source.RuntimeID.Ref<ConfigRef>();
            gid = @ref.Gid;

            return TryToFindRuntimeRef<TComponent>(@ref, out findingEnt);
        }

        public static bool TryToFindRuntimeRef<TComponent>(this ConfigRef source, out World<WT>.Entity findingEnt)
            where TComponent : struct, IComponent
        {
            foreach (var ent in W.Query<All<ConfigRef, TComponent>>().Entities())
                if (ent.Ref<ConfigRef>().Equals(source))
                {
                    findingEnt = ent;
                    return true;
                }

            findingEnt = default;
            return false;
        }

        public static TConfigTable GetConfigTable<TConfigTable>(this World<WT>.Entity source)
            where TConfigTable : struct, IComponent
        {
            return source.Ref<ConfigRef>().GetConfigTable<TConfigTable>();
        }

        public static TConfigTable GetConfigTable<TConfigTable>(this ConfigRef source)
            where TConfigTable : struct, IComponent
        {
            return source.Gid.Unpack<WCT>().Ref<TConfigTable>();
        }
    }
}
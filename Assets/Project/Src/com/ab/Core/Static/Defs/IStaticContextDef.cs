namespace com.ab.complexity.core
{
    public abstract class IStaticContextDef<TSettings>
    {
        public static void ContextSet(TSettings def) =>
            W.Context<TSettings>.Set(def);

        public static TSettings ContextGet() =>
            W.Context<TSettings>.Get();
    }
}
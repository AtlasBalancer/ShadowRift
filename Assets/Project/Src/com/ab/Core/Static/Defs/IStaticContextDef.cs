namespace com.ab.complexity.core
{
    public abstract class IStaticContextDef<TSettings>
    {
        public static void ContextSet(TSettings def) =>
            W.SetResource<TSettings>(def);

        public static TSettings ContextGet() =>
            W.GetResource<TSettings>();
    }
}
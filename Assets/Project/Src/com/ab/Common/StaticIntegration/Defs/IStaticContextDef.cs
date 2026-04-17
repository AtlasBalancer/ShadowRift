namespace com.ab.common
{
    public abstract class IStaticContextDef<TSettings>
    {
        public static void ContextSet(TSettings def)
        {
            W.SetResource(def);
        }

        public static TSettings ContextGet()
        {
            return W.GetResource<TSettings>();
        }
    }
}
using Sirenix.Utilities;

namespace com.ab.domain.craft
{
    public class CraftViewMono : ViewMono
    {
        public CraftItemViewMono[] Items;

        public override void Init()
        {
            Items.ForEach(item => item.Init(false));   
            base.Init();
        }
    }
}
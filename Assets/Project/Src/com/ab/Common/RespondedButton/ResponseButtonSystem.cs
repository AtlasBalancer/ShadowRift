using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public class ResponseButtonSystem : IUpdateSystem
    {
        public int count = 0;
        
        public void Update()
        {
            foreach (var ent in W.Query.Entities<TagAll<ResponseClick>>())
            {
                if (count < 100)
                {
                    count++;
                    continue;
                }

                count = 0;
                
                
                ent.Ref<ResponseButtonRef>().Val.OnComplete();
                ent.ApplyTag<ResponseClick>(false);
            }
        }
    }
}
using com.ab.core;
using FFS.Libraries.StaticEcs;

namespace com.ab.common
{
    public class ResponseButtonSystem : ISystem
    {
        public int count = 0;
        
        public void Update()
        {
            foreach (var ent in W.Query<All<ResponseClick>>().Entities())
            {
                if (count < 100)
                {
                    count++;
                    continue;
                }

                count = 0;
                
                
                ent.Ref<ResponseButtonRef>().Val.OnComplete();
                ent.Apply<ResponseClick>(false);
            }
        }
    }
}
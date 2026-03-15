using System;
using UnityEngine;
using com.ab.complexity.core;
using com.ab.domain.craft;
using Project.Src.com.ab.Domain.Unit.Items;

namespace Project.Src.com.ab.Domain.ItemTable
{
    [CreateAssetMenu(fileName = "#Name#ItemTableEntryDef", menuName = "com.ab/itemTable/def")]
    public class ItemTableEntryDef : StaticEntrySOParamDef<ItemTableEntryDef.Settings>, IStaticContextSetDef
    {
        [Serializable]
        public class Settings
        {
            public DropTableService.Settings ItemTableSystem;
        }

        public void SetContext()
        {
            W.Context<DropTableService>.Set(new DropTableService(Def.ItemTableSystem));
        }
    }
}
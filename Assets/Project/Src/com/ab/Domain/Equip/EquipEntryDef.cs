using System;
using com.ab.complexity.core;
using com.ab.domain.equip;
using Project.Src.com.ab.Domain.Equip.Pupet;

namespace Project.Src.com.ab.Domain.Unit.Items
{
    public class EquipEntryDef : StaticEntryParamDef<EquipEntryDef.Settings>,
        IStaticRegisterTypeDef, IStaticUpdateDef
    {
        [Serializable]
        public class Settings
        {
            public EquipPuppetSystem.Settings EquipPuppetSystem;
        }

        public void RegisterType()
        {
            W.RegisterTagType<EquipTag>();
            
            W.Events.RegisterEventType<EquipSetEvent>();
            W.Events.RegisterEventType<EquipUnSetEvent>();
        }

        public void RegisterUpdate()
        {
            SysReg.AddUpdate(new EquipSystem());
            SysReg.AddUpdate(new EquipPuppetSystem(Def.EquipPuppetSystem));
        }
    }
}
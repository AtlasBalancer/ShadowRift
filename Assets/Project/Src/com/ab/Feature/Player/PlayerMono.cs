using com.ab.common;
using com.ab.complexity.core;
using com.ab.complexity.player;
using com.ab.domain.harv;
using UnityEngine;
using Project.Src.com.ab.Domain.Unit.Items;
using Sirenix.OdinInspector;

namespace com.ab.complexity.features.player
{
    public class PlayerMono : EntityLink
    {
        public W.Entity Ent;

        public Transform ToolRoot;

        public Animator Animator;
        public HarvCollectorMono Harvester;

        public EquipMono ToolEquip;

        [Button]
        public void SetTool()
        {
            if (Ent.HasAllOf<Tool>())
                Ent.Delete<Tool>();
            else
                Ent.Add<Tool>();
        }

        public void SetTool(EquipMono tool)
        {
            RemoveTool();

            ToolEquip = Instantiate(tool);
            ToolEquip.transform.SetParent(ToolRoot, false);
        }

        public void RemoveTool()
        {
            if (ToolEquip != null)
            {
                Destroy(ToolEquip.gameObject);
                ToolEquip = null;
            }
        }
    }
}
using com.ab.complexity.core;
using com.ab.complexity.player;
using UnityEngine;
using Project.Src.com.ab.Domain.Harvest;
using Project.Src.com.ab.Domain.Unit.Items;
using Sirenix.OdinInspector;

namespace com.ab.complexity.features.player
{
    public class PlayerMono : MonoBehaviour
    {
        public W.Entity Ent;

        public Transform ToolRoot;

        public Animator Animator;
        public HarvestCollectorMono Harvester;

        public EquipmentMono ToolEquipment;

        [Button]
        public void SetTool()
        {
            if (Ent.HasAllOf<Tool>())
                Ent.Delete<Tool>();
            else
                Ent.Add<Tool>();
        }

        public void SetTool(EquipmentMono tool)
        {
            RemoveTool();

            ToolEquipment = Instantiate(tool);
            ToolEquipment.transform.SetParent(ToolRoot, false);
        }

        public void RemoveTool()
        {
            if (ToolEquipment != null)
            {
                Destroy(ToolEquipment.gameObject);
                ToolEquipment = null;
            }
        }
    }
}
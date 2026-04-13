using com.ab.core;
using UnityEngine;
using UnityEngine.UI;

namespace com.ab.domain.equip
{
    public class EquipPuppetMono : ViewMono
    {
        public RectTransform ToolSlotRoot;
        public RectTransform ToolPuppetRoot;

        public Image ToolSlotIcon;
        public Image ToolPuppetIcon;

        public void SetTool(Image icon)
        {
            RemoveTool();
            
            ToolSlotIcon = Instantiate(icon, ToolSlotRoot);
            ToolPuppetIcon = Instantiate(icon, ToolPuppetRoot);
        }

        public void RemoveTool()
        {
            if (ToolSlotIcon != null)
            {
                Destroy(ToolSlotIcon);
                ToolSlotIcon = null;
            }

            if (ToolPuppetIcon != null)
            {
                Destroy(ToolPuppetIcon);
                ToolPuppetIcon = null;
            }
        }
    }
}
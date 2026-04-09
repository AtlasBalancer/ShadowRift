using FFS.Libraries.StaticEcs;
using com.ab.domain.equip;
using com.ab.complexity.features.player;

namespace Project.Src.com.ab.Domain.Unit.Items
{
    public class EquipSystem : ISystem
    {
        PlayerMono _player;
        EquipPuppetMono _puppet;

        // DropTableService _dropTable;

        public void Init()
        {
            // _dropTable = W.Context<DropTableService>.Get();
            // W.Query.Entities<All<PlayerRef>>().First(out var playerEnt);

            // _player = playerEnt.Ref<PlayerRef>().Ref;
            // _puppet = W.Context<EquipPuppetMono>.Get();
        }

        public void Update()
        {
            // foreach (var ent in W.Query.Entities<All<EquipCommand>>())
            // {

                // W.Query.Entities<TagAll<Equip>>().DeleteTagForAll<Equip>();
                
                
                // ItemDefID id = ent.Ref<InventoryItem>().ID;
                // Image icon = _itemTable.Def.InventoryCards.Items[id].Icon;

                // _puppet.SetTool(icon);

                // var toolPrefab = _itemTable.Def.Equipment.Entrys[id].Prefab;
                // _player.SetTool(toolPrefab);

                // if (!_player.Ent.HasAllOf<Tool>())
                    // _player.Ent.Add<Tool>();

                // ent.Apply<Equip>(true);
                // ent.Delete<EquipCommand>();
            }

            // foreach (var ent in W.Query.Entities<All<UnEquipCommand>>())
            // {
                // foreach (var entEquipped in W.Query.Entities<TagAll<Equip>>())
                    // entEquipped.DeleteTag<Equip>();

                // _player.RemoveTool();

                // if (_player.Ent.HasAllOf<Tool>())
                    // _player.Ent.Delete<Tool>();

                // _puppet.RemoveTool();

                // ent.Apply<Equip>(false);
                // ent.Delete<UnEquipCommand>();
            // }
        // }
    }
}
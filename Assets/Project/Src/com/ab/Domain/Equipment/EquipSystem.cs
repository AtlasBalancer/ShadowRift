using com.ab.complexity.core;
using com.ab.complexity.features.player;
using com.ab.complexity.player;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Equipment;
using Project.Src.com.ab.Domain.Inventory;

namespace Project.Src.com.ab.Domain.Unit.Items
{
    public class EquipSystem : IInitSystem, IUpdateSystem
    {
        PlayerMono _player;
        EquipInventoryPuppetViewMono _puppet;

        // DropTableService _dropTable;

        public void Init()
        {
            // _dropTable = W.Context<DropTableService>.Get();
            W.Query.Entities<All<PlayerRef>>().First(out var playerEnt);

            _player = playerEnt.Ref<PlayerRef>().Ref;
            _puppet = W.Context<EquipInventoryPuppetViewMono>.Get();
        }

        public void Update()
        {
            foreach (var ent in W.Query.Entities<All<EquipCommand>>())
            {

                W.Query.Entities<TagAll<Equipped>>().DeleteTagForAll<Equipped>();
                
                
                // ItemDefID id = ent.Ref<InventoryItem>().ID;
                // Image icon = _itemTable.Def.InventoryCards.Items[id].Icon;

                // _puppet.SetTool(icon);

                // var toolPrefab = _itemTable.Def.Equipment.Entrys[id].Prefab;
                // _player.SetTool(toolPrefab);

                if (!_player.Ent.HasAllOf<Tool>())
                    _player.Ent.Add<Tool>();

                ent.ApplyTag<Equipped>(true);
                ent.Delete<EquipCommand>();
            }

            foreach (var ent in W.Query.Entities<All<UnEquipCommand>>())
            {
                foreach (var entEquipped in W.Query.Entities<TagAll<Equipped>>())
                    entEquipped.DeleteTag<Equipped>();

                _player.RemoveTool();

                if (_player.Ent.HasAllOf<Tool>())
                    _player.Ent.Delete<Tool>();

                _puppet.RemoveTool();

                ent.ApplyTag<Equipped>(false);
                ent.Delete<UnEquipCommand>();
            }
        }
    }
}
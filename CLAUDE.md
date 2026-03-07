# ShadowRift (IdleRift) — CLAUDE.md

## Project Overview

Unity 6 (6000.3.3f1) idle/progression game. Player arrives on an island through an exploding portal, collects shards, harvests resources (wood/ore), crafts tools, and progresses via automation. All gameplay logic runs through **Static ECS** — not MonoBehaviour-heavy code.

**Source root:** `Assets/Project/Src/com/ab/`
**Unity version:** 6000.3.3f1
**Render pipeline:** URP
**Input:** Unity Input System

---

## Architecture

### ECS Framework

Uses `com.felid-force-studios.static-ecs` (FFS StaticECS).

Key types in `Core/Static/Integrations/`:
- `W` — World singleton (`World<T>`)
- `Sys` — Systems singleton (`W.Systems<SysT>`)
- `T` — World type marker
- `SysT` — Systems type marker

### Module Registration Pattern

Every game system has an `*EntryDef.cs` that implements one or more interfaces:

| Interface | Method | Purpose |
|-----------|--------|---------|
| `IStaticRegisterTypeDef` | `RegisterType()` | Register components/events |
| `IStaticTagDef` | `RegisterTag()` | Register tags |
| `IStaticContextSetDef` | `SetContext()` | Set shared context data |
| `IStaticInitDef` | `RegisterInit()` | One-time init systems |
| `IStaticUpdateDef` | `RegisterUpdate()` | Per-frame update systems |
| `IStaticCreateEntityDef` | `CreateEntities()` | Spawn initial entities |

`Startup.cs` discovers all modules via `_def.Modules` (ScriptableObjects) and features via `_def.Features` (GameObjects), then calls each interface method in order.

### Initialization Order (Startup.Awake)

1. `W.Create()` → create world
2. `RegisterCoreTypes()` → register `Ref` component
3. `RegisterTypes()` + `RegisterTag()` → all module types
4. `AutoRegister<T>.Apply()` + `W.Initialize()`
5. `SetContext()` → populate `W.Context<T>` values
6. `CreateEntities()` → spawn initial entities
7. `Sys.Create()` → create systems container
8. `RegisterInits()` + `RegisterUpdates()` → register systems
9. `Sys.Initialize()` → finalize

`Update()` calls `Sys.Update()` each frame. `OnDestroy()` tears down.

---

## Directory Structure

```
Assets/Project/Src/com/ab/
├── Common/          Shared components (Position, Velocity, Timer, AnimatorRef, etc.)
├── Core/            ECS framework wrappers (W, Sys, Startup, extensions, interfaces)
├── Domain/
│   ├── Craft/       Crafting system
│   ├── Equipment/   Tool equip/change system
│   ├── Harvest/     Resource harvesting (trees, ore)
│   ├── Inventory/   Item inventory management
│   ├── ItemTable/   ScriptableObject item definitions
│   ├── Placed/      Collectible item spawning
│   └── Topdown/     Top-down movement systems
├── Feature/
│   └── Player/      Player entity creation and MonoBehaviour
├── Interactions/    Input → ECS (joystick to movement)
└── Mono/            Base MonoBehaviour helpers
```

---

## Naming Conventions

| Suffix/Pattern | Example | Meaning |
|----------------|---------|---------|
| `*EntryDef` | `InventoryEntryDef` | Module registration entry point (ScriptableObject or plain class) |
| `*System` | `MovementVelocitySystem` | ECS system (processes entities each frame) |
| `*Mono` | `HarvestableMono` | MonoBehaviour — view or bridge to ECS |
| `*Component` / plain struct | `Position`, `Velocity` | ECS component (data) |
| `*Tag` | `PlayerTag`, `Equipped` | ECS tag (boolean flag) |
| `*So` | `HarvestItemSo` | ScriptableObject data definition |
| `*Table` | `HarvestItemTable` | Data lookup table MonoBehaviour/SO |
| `*Command` | `EquipCommand`, `CraftCommand` | Action request component |
| `*Service` | `InventoryService` | Context data class stored in `W.Context<T>` |

**Namespaces:**
- `com.ab.complexity.core` — ECS core wrappers
- `com.ab.complexity.domain.*` — Domain systems
- `com.ab.complexity.features.player` — Player feature
- `com.ab.common` — Shared utilities

---

## Adding a New System

1. Create `*EntryDef.cs` implementing the needed interfaces.
2. Add component/tag/event registrations in `RegisterType()` / `RegisterTag()`.
3. Create system class(es) and add in `RegisterUpdate()` / `RegisterInit()`.
4. Reference the EntryDef in `Startup._def.Modules` (SO) or `Startup._def.Features` (GO) in the scene.

No need to modify `Startup.cs` — it auto-discovers via interface.

---

## ECS Patterns

### Creating an entity
```csharp
var ent = W.Entity.New();
ent.SetTag<PlayerTag>();
ent.Add(new Position { Value = Vector2.zero });
```

### Querying entities in a system
```csharp
// Typical pattern — use W.QueryEntities or foreach with masks
foreach (var entity in W.QueryEntities<PlayerTag>()) { ... }
```

### Shared state (context)
```csharp
// Set in SetContext():
W.Context<InventoryService>.Set(new InventoryService());

// Read anywhere:
var inv = W.Context<InventoryService>.Get();
```

### Events
```csharp
// Register: WEvents.RegisterEventType<InventoryAddMaterial>();
// Send: W.Event.Send(new InventoryAddMaterial { ... });
```

---

## Key Packages

- `com.felid-force-studios.static-ecs` — core ECS
- `com.felid-force-studios.static-ecs-unity` — Unity integration + `EcsDebug`, `AutoRegister`
- `com.unity.inputsystem` — Input handling
- `com.unity.2d.*` — 2D rendering, tilemaps, animation
- `com.unity.render-pipelines.universal` — URP
- Odin Inspector (`Sirenix.OdinInspector`) — editor tooling

---

## Current Systems Status

| System | Status |
|--------|--------|
| Topdown movement | Done |
| Harvest (trees, ore) | Done (prototype) |
| Inventory display | Done |
| Crafting (pickaxes) | Done |
| Equipment (equip/swap tools) | Done |
| Save system | In progress (commit: "Before using claude") |
| Idle progression mechanics | Pending |

---

## Important Notes

- **Inventory cells** should be implemented via ECS entities to support filters — noted 04.03.2026.
- The game concept is an idle game (working title: **IdleRift**). Player goal: return home through the portal.
- Mining is prototype-level; full progression loop not yet implemented.
- Docs are at `Assets/Project/Docs/` — `dev-notes.md` (Russian), `tasks.md`, `ItemDefScheme.puml`.

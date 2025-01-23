using Content.Shared.Starlight.GhostTheme;
using Content.Shared.Starlight;
using Content.Shared.Ghost;
using Content.Shared.Weapons.Ranged.Systems;
using Robust.Client.GameObjects;
using Robust.Shared.Prototypes;

namespace Content.Client.Starlight.GhostTheme;

public sealed class GhostThemeSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly ISharedPlayersRoleManager _playerManager = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<GhostThemeComponent, AfterAutoHandleStateEvent>(OnChanged);
        SubscribeNetworkEvent<GhostThemeSyncEvent>(OnSync);
    }

    private void OnChanged(EntityUid uid, GhostThemeComponent component, ref AfterAutoHandleStateEvent args)
    {
        SyncTheme(uid, component);
    }
    
    private void OnSync(GhostThemeSyncEvent args)
    {
        var uid = GetEntity(args.Player);
        
        if (EntityManager.HasComponent<GhostComponent>(uid))
        {
            var component = EntityManager.EnsureComponent<GhostThemeComponent>(uid);
            SyncTheme(uid, component);
        }
    }
    
    private void SyncTheme(EntityUid uid, GhostThemeComponent component)
    {
        if (component.SelectedGhostTheme == "None" 
            || !_prototypeManager.TryIndex<GhostThemePrototype>(component.SelectedGhostTheme, out var ghostThemePrototype) 
            || !EntityManager.TryGetComponent<SpriteComponent>(uid, out var sprite)
            || sprite.LayerMapTryGet(EffectLayers.Unshaded, out var layer) 
            || !_playerManager.HasAnyPlayerFlags(uid, ghostThemePrototype.Flags))
            return;

        sprite.LayerSetSprite(layer, ghostThemePrototype.SpriteSpecifier.Sprite);
        sprite.LayerSetShader(layer, "unshaded");
        sprite.LayerSetColor(layer, ghostThemePrototype.SpriteSpecifier.SpriteColor);
        sprite.LayerSetScale(layer, ghostThemePrototype.SpriteSpecifier.SpriteScale);
        sprite.NoRotation = ghostThemePrototype.SpriteSpecifier.SpriteRotation;

        sprite.DrawDepth = DrawDepth.Default + 11;
        sprite.OverrideContainerOcclusion = true;
    }
}
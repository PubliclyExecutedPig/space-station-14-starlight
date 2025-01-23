using Content.Client.Lobby;
using Content.Shared.Clothing;
using Content.Shared.Humanoid.Prototypes;
using Content.Shared.Humanoid;
using Content.Shared.Preferences.Loadouts;
using Content.Shared.Preferences;
using Content.Shared.Roles;
using Robust.Client.AutoGenerated;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;
using System.Linq;

namespace Content.Client._Starlight.GhostTheme;

[GenerateTypedNameReferences]
public sealed partial class GhostPicker : ContainerButton
{
    public GhostPicker(
        SpriteSystem spriteSystem,
        SpriteSpecifier icon,
        string name,
        bool isDisabled)
    {
        RobustXamlLoader.Load(this);
        AddStyleClass(StyleClassButton);
        
        Disabled = isDisabled;
        View.Texture = spriteSystem.Frame0(icon);
        DescriptionLabel.Text = name;
    }
}
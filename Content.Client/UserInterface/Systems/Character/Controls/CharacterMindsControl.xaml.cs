using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.UserInterface.Systems.Character.Controls;

[GenerateTypedNameReferences]
public sealed partial class CharacterMindsControl : BoxContainer
{
    public CharacterMindsControl()
    {
        RobustXamlLoader.Load(this);
    }
}

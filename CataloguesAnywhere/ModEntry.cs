using CataloguesAnywhere.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace CataloguesAnywhere;

internal class ModEntry : Mod
{
    private ModConfig Config;

    public override void Entry(IModHelper helper)
    {
        this.Config = this.Helper.ReadConfig<ModConfig>();
        helper.Events.Input.ButtonPressed += this.OnButtonPressed;
    }

    private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
    {
        if (!Context.CanPlayerMove || !this.Config.Enabled)
            return;

        var input = this.Helper.Input;
        if (input.IsDown(this.Config.ActivateButton))
        {
            if (input.IsDown(this.Config.FurnitureButton))
                Utility.TryOpenShopMenu("Furniture Catalogue", null as string);
            else if (input.IsDown(this.Config.WallpaperButton))
                Utility.TryOpenShopMenu("Catalogue", null as string);
        }
    }
}

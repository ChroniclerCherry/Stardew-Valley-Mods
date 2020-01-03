using StardewValley;
using StardewValley.Menus;
using System.Collections.Generic;

namespace ShopTileFramework
{
    class AnimalShop
    {
        private List<StardewValley.Object> ShopAnimalStock;
        private List<StardewValley.Object> AllAnimalsStock;
        private AnimalShopPack ShopPack;
        public AnimalShop(AnimalShopPack ShopPack, string ShopName)
        {
            this.ShopPack = ShopPack;
        }

        private void UpdateShopAnimalStock()
        {
            AllAnimalsStock = new List<StardewValley.Object>();

            if (ModEntry.BFAV == null || !ModEntry.BFAV.IsEnabled())
            {
                AllAnimalsStock = Utility.getPurchaseAnimalStock();
            }
            else
            {
                AllAnimalsStock = ModEntry.BFAV.GetAnimalShopStock(Game1.getFarm());
            }

            ShopAnimalStock = new List<StardewValley.Object>();
            foreach (var animal in AllAnimalsStock)
            {
                if (ShopPack.AnimalStock.Contains(animal.Name))
                {
                    ShopAnimalStock.Add(animal);
                }
            }
        }

        internal void DisplayShop()
        {
            //get animal stock each time to refresh requirement checks
            UpdateShopAnimalStock();
            ModEntry.SourceLocation = Game1.currentLocation;
            Game1.activeClickableMenu = new PurchaseAnimalsMenu(ShopAnimalStock);
        }
    }

    public interface BFAVApi
    {
        bool IsEnabled();
        List<StardewValley.Object> GetAnimalShopStock(Farm farm);
        Dictionary<string, List<string>> GetFarmAnimalCategories();

    }
}
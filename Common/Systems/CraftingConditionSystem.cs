using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStorage.Common;
using TerraStorage.Content.UI.Elements;

namespace TerraStorageOverflow.Common.Systems
{
    public class CraftingConditionSystem : ModSystem
    {
        public override void Load()
        {
            MethodInfo getItemCondition = typeof(ItemSearchHelper).GetMethod("GetItemCondition",
                BindingFlags.NonPublic | BindingFlags.Static);

            if (getItemCondition != null)
            {
                MonoModHooks.Add(getItemCondition, Detour_GetItemCondition);
            }
        }

        private delegate CraftingCondition orig_GetItemCondition(int itemType);

        private static CraftingCondition Detour_GetItemCondition(orig_GetItemCondition orig, int itemType)
        {
            return IsNearWater(itemType)
                ? CraftingCondition.NearWater
                : IsNearLava(itemType) ? CraftingCondition.NearLava : IsNearHoney(itemType) ? CraftingCondition.NearHoney : orig(itemType);
        }

        private static bool IsNearWater(int itemType)
        {
            Item item = new();
            item.SetDefaults(itemType);

            return item.createTile >= TileID.Dirt && TileID.Sets.CountsAsWaterSource[item.createTile];
        }

        private static bool IsNearLava(int itemType)
        {
            Item item = new();
            item.SetDefaults(itemType);

            return item.createTile >= TileID.Dirt && TileID.Sets.CountsAsLavaSource[item.createTile];
        }

        private static bool IsNearHoney(int itemType)
        {
            Item item = new();
            item.SetDefaults(itemType);

            return item.createTile >= TileID.Dirt && TileID.Sets.CountsAsHoneySource[item.createTile];
        }
    }
}
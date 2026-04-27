using Terraria;

namespace TerraStorageOverflow.Common.Utils
{
    public class InventoryUtils
    {
        public static bool HasRoomForItem(Item item)
        {
            bool hasSpace = false;
            for (int i = 0; i < 50; i++)
            {
                if (Main.LocalPlayer.inventory[i].IsAir)
                {
                    hasSpace = true;
                }
                if (Main.LocalPlayer.inventory[i].type == item.type && Main.LocalPlayer.inventory[i].stack < Main.LocalPlayer.inventory[i].maxStack)
                {
                    hasSpace = true;
                }

            }
            return hasSpace;
        }
    }
}

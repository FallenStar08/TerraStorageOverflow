using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerraStorageOverflow.Common.Systems;

namespace TerraStorageOverflow.Common.GlobalItems
{
    public class StorageVacuumLogic : GlobalItem
    {
        public override bool ItemSpace(Item item, Player player)
        {
            return player.GetModPlayer<ModPlayers.TerraStorageOverflow>().HasActiveStorage || base.ItemSpace(item, player);
        }

        public override bool GrabStyle(Item item, Player player)
        {
            var modPlayer = player.GetModPlayer<ModPlayers.TerraStorageOverflow>();

            if (player.whoAmI == Main.myPlayer && modPlayer.HasActiveStorage)
            {
                if (!player.CanAcceptItemIntoInventory(item))
                {
                    Vector2 toPlayer = player.Center - item.Center;
                    float distSq = toPlayer.LengthSquared();

                    int grabRange = player.GetItemGrabRange(item);
                    float rangeSq = (float)grabRange * grabRange;

                    if (distSq < rangeSq)
                    {
                        if (Main.GameUpdateCount % 60 == 0)
                            StorageConfig.Log($"[TS] Magnetizing {item.Name} (Range: {grabRange})", Color.Pink);

                        toPlayer.Normalize();
                        item.velocity = toPlayer * (12f + player.moveSpeed);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
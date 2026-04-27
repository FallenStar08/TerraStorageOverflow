using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStorageOverflow.Common.Systems;
using TerraStorageOverflow.Common.Utils;

namespace TerraStorageOverflow.Common.GlobalItems
{
    public class StorageVacuumLogic : GlobalItem
    {
        private bool IsInstantPickup(Item item)
        {
            return (item.type > ItemID.None && item.type < ItemID.Count && ItemID.Sets.IsAPickup[item.type])
                   || item.maxStack <= 0;
        }

        public override bool ItemSpace(Item item, Player player)
        {
            return IsInstantPickup(item)
                ? base.ItemSpace(item, player)
                : player.GetModPlayer<ModPlayers.TerraStorageOverflow>().HasActiveStorage || base.ItemSpace(item, player);
        }

        public override bool GrabStyle(Item item, Player player)
        {
            var modPlayer = player.GetModPlayer<ModPlayers.TerraStorageOverflow>();
            if (player.whoAmI == Main.myPlayer && modPlayer.HasActiveStorage)
            {
                if (!InventoryUtils.HasRoomForItem(item) && !IsInstantPickup(item))
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
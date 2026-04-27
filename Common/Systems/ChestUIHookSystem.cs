using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TerraStorageOverflow.Common.Utils;

namespace TerraStorageOverflow.Common.Systems
{
    public class ChestUIHookSystem : ModSystem
    {
        public override void Load()
        {
            MethodInfo lootAll = typeof(ChestUI).GetMethod("LootAll", BindingFlags.Public | BindingFlags.Static);

            if (lootAll != null)
            {
                MonoModHooks.Add(lootAll, Detour_LootAll);
                Loggers.Log("[TS] Hooked ChestUI.LootAll successfully.");
            }
        }

        private void Detour_LootAll(Action orig)
        {

            orig();

            Player player = Main.LocalPlayer;
            var modPlayer = player.GetModPlayer<ModPlayers.TerraStorageOverflow>();

            if (!modPlayer.HasActiveStorage) return;

            int chestIndex = player.chest;
            if (chestIndex <= -1) return;

            Item[] chestInv = chestIndex == -2
                ? player.bank.item
                : chestIndex == -3
                ? player.bank2.item
                : chestIndex == -4 ? player.bank3.item : chestIndex == -5 ? player.bank4.item : Main.chest[chestIndex].item;


            for (int i = 0; i < chestInv.Length; i++)
            {
                Item item = chestInv[i];

                if (!item.IsAir && !InventoryUtils.HasRoomForItem(item))
                {
                    Loggers.Log($"[TS] Loot All Overflow: {item.Name} -> Storage.", Color.Orange);

                    if (modPlayer.DepositIntoAllNetworks(item))
                    {
                        chestInv[i] = new Item();

                        if (Main.netMode == NetmodeID.MultiplayerClient && chestIndex >= 0)
                        {
                            NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, chestIndex, i);
                        }
                    }
                }
            }
        }
    }
}
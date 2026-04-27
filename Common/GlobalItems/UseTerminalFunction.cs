using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerraStorage.Content.Items;
using TerraStorage.Content.Tiles;
using TerraStorage.Content.UI;
using TerraStorageOverflow.Common.Utils;

namespace TerraStorageOverflow.Common.GlobalItems
{
    public class UseTerminalFunction : GlobalItem
    {
        public override void HoldItem(Item item, Player player)
        {
            if (player.whoAmI != Main.myPlayer) return;

            if (item.ModItem is RemoteTerminal rt)
            {
                if (Main.mouseLeft && Main.mouseLeftRelease && !Main.LocalPlayer.mouseInterface)
                {
                    if (rt.BoundEntityId != -1 && TileEntity.ByID.TryGetValue(rt.BoundEntityId, out var te))
                    {
                        if (te is TerminalEntity terminal)
                        {
                            TerminalUISystem instance = ModContent.GetInstance<TerminalUISystem>();
                            if (instance == null)
                            {
                                return;
                            }
                            instance.OpenTerminalRemote(terminal);

                            Loggers.Log("[TS] HoldItem: Terminal UI opened via manual click detection.", Color.MediumPurple);
                        }
                    }
                    else
                    {
                        if (Main.GameUpdateCount % 60 == 0)
                            Loggers.Log("[TS] Cannot open: Remote is not bound.", Color.Red);
                    }
                }
            }
        }
    }
}
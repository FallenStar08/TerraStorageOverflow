using System.ComponentModel;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace TerraStorageOverflow.Common.Systems
{
    public class ModSettings : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(false)]
        public bool DebugText;

        public static void Log(string message, Color? color = null)
        {
            if (ModContent.GetInstance<ModSettings>().DebugText)
            {
                Main.NewText(message, color ?? Color.White);
            }
        }
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerraStorageOverflow.Common.Systems;

namespace TerraStorageOverflow.Common.Utils
{
    public class Loggers
    {
        public static void Log(string message, Color? color = null)
        {
            if (ModContent.GetInstance<ModSettings>().DebugText)
            {
                Main.NewText(message, color ?? Color.White);
            }
        }
    }
}

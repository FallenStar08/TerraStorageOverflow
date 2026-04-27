using System.Runtime.CompilerServices; // Required for CallerMemberName
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerraStorageOverflow.Common.Systems;

namespace TerraStorageOverflow.Common.Utils
{
    public class Loggers
    {
        public static void Log(string message, Color? color = null, [CallerMemberName] string caller = "")
        {
            if (ModContent.GetInstance<ModSettings>().DebugText)
            {
                string prefix = $"[TSO] {caller}(): ";
                Main.NewText(prefix + message, color ?? Color.White);
            }
        }
    }
}
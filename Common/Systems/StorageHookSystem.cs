using System;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStorage.Content.Tiles;

namespace TerraStorageOverflow.Common.Systems
{
    [ExtendsFromMod("TerraStorage")]
    public class StorageHookSystem : ModSystem
    {
        private delegate bool orig_InsertDisk(DriveBayEntity self, Item diskItem, int slot);
        private delegate Item orig_RemoveDisk(DriveBayEntity self, int slot);
        public override void Load()
        {
            Type driveBayType = typeof(DriveBayEntity);

            MethodInfo insertMethod = driveBayType.GetMethod("InsertDisk", BindingFlags.Public | BindingFlags.Instance);
            MethodInfo removeMethod = driveBayType.GetMethod("RemoveDisk", BindingFlags.Public | BindingFlags.Instance);

            if (insertMethod != null)
            {
                MonoModHooks.Add(insertMethod, Detour_InsertDisk);
            }

            if (removeMethod != null)
            {
                MonoModHooks.Add(removeMethod, Detour_RemoveDisk);
            }
        }

        private bool Detour_InsertDisk(orig_InsertDisk orig, DriveBayEntity self, Item diskItem, int slot)
        {
            bool result = orig(self, diskItem, slot);

            if (result)
            {
                ModPlayers.TerraStorageOverflow.NetworkDirty = true;
                StorageConfig.Log("[TS] Manual Hook: Disk Inserted. Dirty flag set.", Microsoft.Xna.Framework.Color.LightPink);
            }

            return result;
        }

        private Item Detour_RemoveDisk(orig_RemoveDisk orig, DriveBayEntity self, int slot)
        {
            Item result = orig(self, slot);

            StorageConfig.Log($"[TS] Hook: RemoveDisk called for slot {slot}. Result Type: {result.type} (Name: {result.Name})", Microsoft.Xna.Framework.Color.Gray);

            if (result != null && result.type != ItemID.None)
            {
                ModPlayers.TerraStorageOverflow.NetworkDirty = true;
                StorageConfig.Log($"[TS] Hook: Disk Removed ({result.Name}). Network marked dirty.", Microsoft.Xna.Framework.Color.LightPink);
            }

            return result;
        }
    }
}
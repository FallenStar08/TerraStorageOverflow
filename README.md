# TerraStorage Overflow

A small addon for **TerraStorage**. Instead of leaving items on the ground or manually clicking through menus, this mod turns your Remote Terminal into a smart vacuum for your storage network.

## 🛠 Features

* **Smart Overflow:** Automatically redirects picked-up items to your storage disks if your inventory is full, similar to what the void bag does. 
* **Multi-Network Support:** The mod intelligently cycles through all bound networks until the item finds a home.
* **Improved Interaction:**
    * **Left-Click to Open:** No more middle-clicking in the inventory. Use the remote like a normal item to open the UI.
    * **Shift-Click Redirect:** Shift-clicking from chests will bypass your inventory and go straight to the network if you're out of space.
    * **Loot All Integration:** Hit "Loot All" on a chest and watch the excess fly directly into your network.
* **Live Tooltips:** Hover over a remote to see its bound coordinates and a real-time capacity readout (Used / Total).
* **Multiplayer Optimized:** Uses a custom buffering system to batch item deposits. This prevents packet-spam and should keep the server smooth even during mass-looting, probably.

## Performance
Should be fine? I use event driven caching instead of constant pooling.

## Requirements
* **tModLoader** (v2022.9+ / 1.4.4)
* **TerraStorage** (Base Mod)

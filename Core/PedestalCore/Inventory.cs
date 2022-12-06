using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TWitchery.PedestalCore;
class Inventory : StackedInventory {
	public Inventory() : base(1) {}
	public Item Slot {
		get => slots[0];
		set => slots[0] = value;
	}
	public PedestalCore.Action Interract(int i, int j, Player ply, Item[] inv, int slot) {
		if (inv[slot].type == ModContent.ItemType<Items.EbonWand>())
			return PedestalCore.Action.ShowLink;
		return (PedestalCore.Action)BasicInterract(i, j, ply, inv, slot);
	}
}
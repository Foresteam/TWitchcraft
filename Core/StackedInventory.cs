using Terraria;
using Terraria.ID;
using System.Linq;

namespace TWitchery;
class StackedInventory {
	public Item[] slots;

	public virtual int SlotsUsed => slots.Sum(i => i.type == ItemID.None ? 0 : 1);

	public StackedInventory(int size) {
		slots = Enumerable.Repeat(new Item(), size).ToArray();
	}

	protected virtual bool TryPut(Item item) {
		for (int i = 0; i < slots.Length; i++)
			if (slots[i].type == 0) {
				slots[i] = item;
				return true;
			}
		return false;
	}
	public bool Put(ref Item activeItem) {
		if (!TryPut(activeItem))
			return false;
		activeItem = new Item();
		return true;
	}

	#nullable enable
	protected virtual Item? Take(bool peek = false) {
		for (int i = slots.Length - 1; i >= 0; i--)
			if (slots[i].type != 0) {
				var t = slots[i];
				if (!peek)
					slots[i] = new Item();
				return t;
			}
		return null;
	}
	public Item? Take(int i, int j, Player ply) {
		Item? heldItem = Take();
		if (heldItem == null)
			return null;
		Item taken = ply.GetItem(Main.myPlayer, heldItem, GetItemSettings.InventoryEntityToPlayerInventorySettings);
		if (!taken.IsAir)
			ply.QuickSpawnItem(null, taken);
		return heldItem;
	}
}
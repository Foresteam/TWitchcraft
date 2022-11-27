using Terraria;
using Terraria.ID;
using System.Linq;

namespace TWitchery;
class StackedInventory {
	public enum Action {
		Nothing = 0,
		Take,
		Put
	}

	public Item[] slots;

	public virtual int SlotsUsed => slots.Sum(i => i.type == ItemID.None ? 0 : 1);

	public StackedInventory(int size) {
		slots = Enumerable.Repeat(new Item(), size).ToArray();
	}

	protected virtual bool TryPut(Item item) {
		for (int i = 0; i < slots.Length; i++) {
			if (slots[i].type == item.type) {
				slots[i].stack += item.stack;
				return true;
			}
			if (slots[i].type == 0) {
				slots[i] = item;
				return true;
			}
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
	public virtual Item? Take(bool peek = false) {
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
		HelpMe.GiveItem(heldItem, ply);
		return heldItem;
	}

	public Action BasicInterract(int i, int j, Player ply, Item[] inv, int slot) {
		if (SlotsUsed > 0 && inv[slot].type == 0)
			return Action.Take;
		return Action.Put;
	}
}
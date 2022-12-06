using Terraria;
using Terraria.ID;
using System.ComponentModel;

namespace TWitchery.CauldronCore;
class Inventory : StackedInventory {
#nullable enable
	public delegate void CatalystSlotHandler(Item item);
	public CatalystSlotHandler? CatalystPut, CatalystTaken;
	public Item catalyst;
	public override int SlotsUsed => base.SlotsUsed + (catalyst.type != ItemID.None ? 1 : 0);
	public Inventory(int size) : base(size) {
		catalyst = new Item();
	}

	public void PutCatalyst(ref Item newCatalyst) {
		if (newCatalyst.type == catalyst.type) {
			catalyst.stack += newCatalyst.stack;
			newCatalyst = new Item();
			return;
		}
		HelpMe.Swap(ref catalyst, ref newCatalyst);
	}

	private bool TryPutCatalyst(Item item) {
		if (catalyst.type == 0) {
			catalyst = item;
			CatalystPut?.Invoke(item);
			return true;
		}
		return false;
	}
	protected override bool TryPut(Item item) {
		return base.TryPut(item) || TryPutCatalyst(item);
	}

	#nullable enable
	public override Item? Take(bool peek = false) {
		if (catalyst.type != 0) {
			var t = catalyst;
			if (!peek)
				catalyst = new Item();
			CatalystTaken?.Invoke(catalyst);
			return t;
		}
		return base.Take(peek);
	}
}
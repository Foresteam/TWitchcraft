using Terraria;
using Terraria.ID;

namespace TWitchery.Cauldron;
class CauldronInventory : StackedInventory {
	public Item catalyst;
	public override int SlotsUsed => base.SlotsUsed + (catalyst.type != ItemID.None ? 1 : 0);
	public CauldronInventory(int size) : base(size) {
		catalyst = new Item();
	}

	public void PutCatalyst(ref Item newCatalyst) {
		HelpMe.Swap(ref catalyst, ref newCatalyst);
	}

	private bool TryPutCatalyst(Item item) {
		if (catalyst.type == 0) {
			catalyst = item;
			return true;
		}
		return false;
	}
	protected override bool TryPut(Item item) {
		return base.TryPut(item) || TryPutCatalyst(item);
	}

	#nullable enable
	protected override Item? Take(bool peek = false) {
		if (catalyst.type != 0) {
			var t = catalyst;
			if (!peek)
				catalyst = new Item();
			return t;
		}
		return base.Take(peek);
	}
}
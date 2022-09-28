using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace Test {
	class WitcheryCrafting {
		public Item[] Slots;
		public Item Catalyst;
		public WitcheryCrafting(int size, bool catalyst) {
			Slots = Enumerable.Repeat(new Item(), size).ToArray();
			Catalyst = catalyst ? new Item() : null;
		}

		public bool Put(Item item) {
			for (int i = 0; i < Slots.Length; i++)
				if (Slots[i].type != 1) {
					Slots[i] = item;
					return true;
				}
			return false;
		}
		public void RMBInterract(int i, int j) {

		}
	}
}
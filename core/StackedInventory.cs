using Terraria;
using System.Linq;

namespace TWitchery {
	public abstract class StackedInventory {
		public Item[] slots;
		#nullable enable
		public Item catalyst;
		protected bool _useCatalyst, _useLiquids;

		public StackedInventory(int size, bool useCatalyst, bool useLiquids = false) {
			_useCatalyst = useCatalyst;
			_useLiquids = useLiquids;
			slots = Enumerable.Repeat(new Item(), size).ToArray();
			catalyst = new Item();
		}

		public abstract void PutCatalyst(ref Item newCatalyst);
		public abstract bool Put(ref Item activeItem);
		public abstract Item? Take(int i, int j, Player ply);
	}
}
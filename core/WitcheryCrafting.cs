using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System;

namespace TWitchery {
	class WitcheryCrafting {
		public enum Action {
			Nothing = 0,
			Take,
			Put,
			PutCatalyst,
			Craft
		}
		public Item[] slots;
		#nullable enable
		public Item catalyst;
		private bool _useCatalyst;
		private List<WitcheryRecipe> _recipes;
		public WitcheryCrafting(int size, bool useCatalyst, List<WitcheryRecipe>? recipes = null) {
			_useCatalyst = useCatalyst;
			slots = Enumerable.Repeat(new Item(), size).ToArray();
			this.catalyst = new Item();
			_recipes = recipes == null ? new List<WitcheryRecipe>() : recipes;
		}

		public int SlotsUsed {
			get => slots.Sum(i => i.type == 0 ? 0 : 1) + (catalyst.type != 0 ? 1 : 0);
		}
		// private void RedefineCatalyst() {
		// 	if (catalyst.type != 0)
		// 		return;
		// 	for (int i = slots.Length - 1; i >= 0; i--)
		// 		if (slots[i].type != 0) {
		// 			catalyst = slots[i];
		// 			slots[i] = new Item();
		// 			return;
		// 		}
		// }
		public void PutCatalyst(ref Item newCatalyst) {
			if (!_useCatalyst)
				return;
			HelpMe.Swap(ref catalyst, ref newCatalyst);
		}
		public bool TryPut(Item item) {
			for (int i = 0; i < slots.Length; i++)
				if (slots[i].type == 0) {
					slots[i] = item;
					return true;
				}
			if (catalyst.type == 0) {
				catalyst = item;
				return true;
			}
			return false;
		}
		public bool Put(ref Item activeItem) {
			if (!TryPut(activeItem))
				return false;
			Main.NewText("Two dicks in my ass is not enough... " + activeItem.type);
			activeItem = new Item();
			return true;
		}

		#nullable enable
		public Item? Take(bool peek = false) {
			if (catalyst.type != 0) {
				var t = catalyst;
				if (!peek)
					catalyst = new Item();
				return t;
			}
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
			Main.NewText($"Contained items: {String.Join(", ", slots.Select(i => $"{i.Name}x{i.stack}").ToArray())}");
			return heldItem;
		}

		public Action Interract(int i, int j, Player ply, Item[] inv, int slot) {
			// take item
			if (SlotsUsed > 0 && inv[slot].type == 0)
				return Action.Take;
			if (inv[slot].type == ItemID.Torch)
				return Action.Craft;
			if (Main.tile[i, j].TileFrameY < 16)
				return Action.PutCatalyst;
			// put item
			return Action.Put;
		}
		public void Craft(int i, int j, Player ply, TileEntity source) {
			// RedefineCatalyst();
			var recipe = WitcheryRecipe.BestMatch(_recipes, slots, catalyst, new Liquid[] {});
			var result = recipe.Craft(slots, catalyst, new Liquid[] {});

			slots = Enumerable.Repeat(new Item(), slots.Length).ToArray();
			catalyst = new Item();

			if (result == null)
				return;
			foreach (var item in result.items)
				ply.QuickSpawnClonedItem(new EntitySource_TileEntity(source), item.self, item.self.stack);
		}
	}
}
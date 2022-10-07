using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System;

namespace TWitchery {
	class WitcheryCrafting : StackedInventory {
		public enum Action {
			Nothing = 0,
			Take,
			Put,
			PutCatalyst,
			Craft
		}
		private List<WitcheryRecipe> _recipes;
		public WitcheryCrafting(int size, bool useCatalyst, bool useLiquids = false, List<WitcheryRecipe> recipes = null) : base(size, useCatalyst, useLiquids) {
			_recipes = recipes == null ? new List<WitcheryRecipe>() : recipes;
		}
		
		public override void PutCatalyst(ref Item newCatalyst) {
			if (!_useCatalyst)
				return;
			HelpMe.Swap(ref catalyst, ref newCatalyst);
		}

		private bool TryPut(Item item) {
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
		public override bool Put(ref Item activeItem) {
			if (!TryPut(activeItem))
				return false;
			activeItem = new Item();
			return true;
		}

		#nullable enable
		private Item? Take(bool peek = false) {
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
		public override Item? Take(int i, int j, Player ply) {
			Item? heldItem = Take();
			if (heldItem == null)
				return null;
			Item taken = ply.GetItem(Main.myPlayer, heldItem, GetItemSettings.InventoryEntityToPlayerInventorySettings);
			if (!taken.IsAir)
				ply.QuickSpawnItem(null, taken);
			return heldItem;
		}

		public Action Interract(int i, int j, Player ply, Item[] inv, int slot) {
			// take item
			if (SlotsUsed > 0 && inv[slot].type == 0)
				return Action.Take;
			if (inv[slot].type == ModContent.ItemType<Items.EbonWand>())
				return Action.Craft;
			if (Main.tile[i, j].TileFrameY < 16)
				return Action.PutCatalyst;
			// put item
			return Action.Put;
		}
		public WitcheryRecipe.Result? Craft() {
			// RedefineCatalyst();
			var recipe = WitcheryRecipe.BestMatch(_recipes, slots, catalyst, new Liquid[] {});
			var result = recipe.Craft(slots, catalyst, new Liquid[] {});
			Flush();

			return result;
		}
		public void Flush() {
			slots = Enumerable.Repeat(new Item(), slots.Length).ToArray();
			catalyst = new Item();
		}
		public static void GiveResult(WitcheryRecipe.Result result, Point16 tile, Player ply, TileEntity source) {
			if (result == null)
				return;
			foreach (var item in result.items)
				ply.QuickSpawnClonedItem(new EntitySource_TileEntity(source), item.self, item.self.stack);
		}
	}
}
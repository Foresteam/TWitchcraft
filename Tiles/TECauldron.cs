using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TWitchery.Tiles {
	public class TECauldron : TEAbstractStation {
		private static List<WitcheryRecipe> _recipes = new(new WitcheryRecipe[] {
			new WitcheryRecipe(energyCost: 0)
				.AddIngredient(new Item(ItemID.DirtBlock, 5))
				.SetCatalyst(new Item(ItemID.Wood, 1))
				.AddResult(new WitcheryRecipe.Result.ItemResult(new Item(ItemID.StonePlatform, 10)))
		});
		private WitcheryCrafting _crafting;
		public StackedInventory Inventory => _crafting;
		public TECauldron() {
			_crafting = new WitcheryCrafting(5, true, true, _recipes);
		}

		public override bool IsValidTile(in Tile tile) => tile.TileType == ModContent.TileType<TestCauldron>();
		protected override void OnPlace(int i, int j) {
			// ass.Add(Main.rand.Next());
			Main.NewText("I exist, therefore i am in the world.");
		}
		public void RightClick(int i, int j) {
			var ply = Main.LocalPlayer;
			int slot = ply.selectedItem;
			var inv = ply.inventory;
			// no mouse yet
			ref var activeItem = ref inv[slot];
			switch (_crafting.Interract(i, j, ply, inv, slot)) {
				case WitcheryCrafting.Action.Take:
					_crafting.Take(i, j, ply);
					break;
				case WitcheryCrafting.Action.Put:
					_crafting.Put(ref activeItem);
					break;
				case WitcheryCrafting.Action.PutCatalyst:
					_crafting.PutCatalyst(ref activeItem);
					break;
				case WitcheryCrafting.Action.Craft:
					_crafting.Craft(i, j, ply, this);
					break;
			}
		}
	}
}
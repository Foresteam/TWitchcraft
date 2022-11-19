using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using TWitchery.Cauldron;

namespace TWitchery.Tiles {
	class TECauldron : TEAbstractStation, IRightClickable {
		private static List<WitcheryRecipe> _recipes = new List<WitcheryRecipe>(new WitcheryRecipe[] {
			new WitcheryRecipe(energyCost: 0)
				.AddIngredient(new Item(ItemID.DirtBlock, 5))
				.SetCatalyst(new Item(ItemID.Wood, 1))
				.AddResult(new WitcheryRecipe.Result.ItemResult(new Item(ItemID.StonePlatform, 10)))
		});
		private CauldronCrafting _crafting;
		public override CauldronInventory Inventory => _crafting.inventory;
		public override LiquidInventory LiquidInventory => _crafting.liquidInventory;
		public TECauldron() {
			_crafting = new CauldronCrafting(5, 5f, _recipes);
		}

		public override bool IsValidTile(in Tile tile) => tile.TileType == ModContent.TileType<TestCauldron>();
		protected override void OnPlace(int i, int j) {
			// ass.Add(Main.rand.Next());
			Main.NewText("I exist, therefore i am in the world.");
		}
		public bool RightClick(int i, int j) {
			var ply = Main.LocalPlayer;
			int slot = ply.selectedItem;
			var inv = ply.inventory;
			// no mouse yet
			ref var activeItem = ref inv[slot];
			switch (_crafting.Interract(i, j, ply, inv, slot)) {
				case CauldronCrafting.Action.Take:
					_crafting.inventory.Take(i, j, ply);
					break;
				case CauldronCrafting.Action.Put:
					_crafting.inventory.Put(ref activeItem);
					break;
				case CauldronCrafting.Action.PutCatalyst:
					_crafting.inventory.PutCatalyst(ref activeItem);
					break;
				case CauldronCrafting.Action.Craft:
					var rs = _crafting.Craft();
					CauldronCrafting.GiveResult(rs, new Terraria.DataStructures.Point16(i, j), ply, this);
					break;
				case CauldronCrafting.Action.Pour: case CauldronCrafting.Action.Draw:
					_crafting.liquidInventory.Apply(ref inv[slot], ply);
					break;
				default:
					return false;
			}
			return true;
		}
	}
}
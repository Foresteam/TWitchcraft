using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System;

namespace TWitchery.Cauldron;
class CauldronCrafting {
	public enum Action {
		Nothing = 0,
		Take,
		Put,
		PutCatalyst,
		Craft
	}
	private List<WitcheryRecipe> _recipes;
	public readonly CauldronInventory inventory;
	public readonly LiquidContainer liquidContainer;
	public CauldronCrafting(int size, bool useCatalyst, bool useLiquids = false, List<WitcheryRecipe> recipes = null) {
		_recipes = recipes == null ? new List<WitcheryRecipe>() : recipes;
		inventory = new CauldronInventory(size);
	}

	public Action Interract(int i, int j, Player ply, Item[] inv, int slot) {
		// take item
		if (inventory.SlotsUsed > 0 && inv[slot].type == 0)
			return Action.Take;
		if (inv[slot].type == ModContent.ItemType<Items.EbonWand>())
			return Action.Craft;
		if (Main.tile[i, j].TileFrameY < 16)
			return Action.PutCatalyst;
		// put item
		return Action.Put;
	}
	#nullable enable
	public WitcheryRecipe.Result? Craft() {
		// RedefineCatalyst();
		var recipe = WitcheryRecipe.BestMatch(_recipes, inventory.slots, inventory.catalyst, new Liquid[] {});
		var result = recipe.Craft(inventory.slots, inventory.catalyst, new Liquid[] {});
		Flush();

		return result;
	}
	public void Flush() {
		inventory.slots = Enumerable.Repeat(new Item(), inventory.slots.Length).ToArray();
		inventory.catalyst = new Item();
	}
	public static void GiveResult(WitcheryRecipe.Result result, Point16 tile, Player ply, TileEntity source) {
		if (result == null)
			return;
		foreach (var item in result.items)
			ply.QuickSpawnClonedItem(new EntitySource_TileEntity(source), item.self, item.self.stack);
	}
}
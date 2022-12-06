using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TWitchery.AltarCore;
using PedestalCore;
partial class Crafting {
	private List<WitcheryRecipe> _recipes;
	public readonly Inventory inventory;
	public Crafting(List<WitcheryRecipe> recipes = null) {
		_recipes = recipes == null ? new List<WitcheryRecipe>() : recipes;
		inventory = new Inventory();
	}

	public Action Interract(int i, int j, Player ply, Item[] inv, int slot) {
		if (inv[slot].type == ModContent.ItemType<Items.EbonWand>())
			return Action.Craft;
		return (Action)inventory.BasicInterract(i, j, ply, inv, slot);
	}
	#nullable enable
	public WitcheryRecipe.Result? Craft(List<Inventory> pedestalInventories) {
		Item[] items = pedestalInventories.Select(i => i.slots.First()).ToArray();
		var recipe = WitcheryRecipe.BestMatch(_recipes, items, inventory.slots.First());
		var result = recipe.Craft(items, inventory.slots.First());

		return result;
	}
	public void Flush(List<Inventory> overallInventory) {
		foreach (var inventory in overallInventory)
			inventory.slots = Enumerable.Repeat(new Item(), inventory.slots.Length).ToArray();
	}
	public void GiveResult(WitcheryRecipe.Result result, Point16 tile, Player ply, TileEntity source) {
		if (result == null)
			return;
		inventory.slots[0] = result.items.First();
		// AltarRecipes should ALWAYS have one item. But for special cases i'll leave this...
		for (int i = 1; i < result.items.Count; i++)
			ply.QuickSpawnClonedItem(new EntitySource_TileEntity(source), result.items[i]);
	}
}
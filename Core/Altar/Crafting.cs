using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace TWitchery.AltarCore;
using PedestalCore;
using Recipes;
partial class Crafting : ICrafting<Crafting.Action, Inventory> {
	private List<WitcheryRecipe> _recipes;
	private Inventory _inventory;
	public Inventory Inventory => _inventory;
	public Crafting(List<WitcheryRecipe> recipes = null) {
		_recipes = recipes == null ? new List<WitcheryRecipe>() : recipes;
		_inventory = new Inventory();
	}

	public Action Interract(int i, int j, Player ply, Item[] inv, int slot) {
		if (
			inv[slot].ModItem is Items.IMagicWand
			&& Inventory.Slot.type != 0
			&& Tiles.TEAltar.GetSatelliteInventories(i, j)?.Sum(i => i.Slot.type == 0 ? 0 : 1) > 0
		)
			return Action.Craft;
		return (Action)_inventory.BasicInterract(i, j, ply, inv, slot);
	}
#nullable enable
	public WitcheryRecipe.Result? Craft(int i, int j) {
		var pedestalInventories = Tiles.TEAltar.GetSatelliteInventories(i, j);
		if (pedestalInventories == null)
			return null;
		Item[] items = pedestalInventories.Select(i => i.Slot).ToArray();
		var recipe = WitcheryRecipe.BestMatch(_recipes, items, _inventory.Slot);
		var result = recipe.Craft(items, _inventory.Slot);

		return result;
	}
	public void Flush(WitcheryRecipe.Result? result, int i, int j) {
		var overallInventory = Tiles.TEAltar.GetOverallInventory(i, j);
		if (overallInventory == null)
			return;
		foreach (var inventory in overallInventory)
			if (result != null && result.energyCost > 0 && Tables.Common.manaPotions.Contains(inventory.Slot.type))
				continue;
			else
				inventory.Slot = new Item();
	}
	public void GiveResult(WitcheryRecipe.Result? result, Point16 tile, Player ply, TileEntity source) {
		if (result == null)
			return;
		_inventory.Slot = result.items.First();
		// AltarRecipes should ALWAYS have one item. But for special cases i'll leave this...
		for (int i = 1; i < result.items.Count; i++)
			ply.QuickSpawnClonedItem(new EntitySource_TileEntity(source), result.items[i]);
	}
}
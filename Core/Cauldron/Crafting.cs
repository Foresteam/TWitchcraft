using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System;

namespace TWitchery.CauldronCore;
using Liquids;
partial class Crafting : ICrafting<Crafting.Action, Inventory> {
	private List<WitcheryRecipe> _recipes;
	private Inventory _inventory;
	public Inventory Inventory => _inventory;
	public readonly LiquidInventory liquidInventory;
	public Crafting(int inventorySize, float volume, List<WitcheryRecipe> recipes = null) {
		_recipes = recipes == null ? new List<WitcheryRecipe>() : recipes;
		_inventory = new Inventory(inventorySize);
		liquidInventory = new LiquidInventory(volume);
	}

	public Action Interract(int i, int j, Player ply, Item[] inv, int slot) {
		// take item
		if (_inventory.SlotsUsed > 0 && inv[slot].type == 0)
			return Action.Take;
		if (inv[slot].type == ModContent.ItemType<Items.EbonWand>())
			return Action.Craft;
		if (Tables.Vessels.vesselsLiquids.Keys.Contains(inv[slot].type))
			return Action.Pour;
		if (Tables.Vessels.vessels.Keys.Contains(inv[slot].type))
			return Action.Draw;
		if (Main.tile[i, j].TileFrameY < 16)
			return Action.PutCatalyst;
		// put item
		return Action.Put;
	}
	#nullable enable
	public WitcheryRecipe.Result? Craft(int i, int j) {
		var recipe = WitcheryRecipe.BestMatch(_recipes, _inventory.slots, _inventory.catalyst, liquidInventory.GetAll());
		var result = recipe.Craft(_inventory.slots, _inventory.catalyst, liquidInventory.GetAll());

		return result;
	}
	public void Flush(int i, int j) {
		_inventory.slots = Enumerable.Repeat(new Item(), _inventory.slots.Length).ToArray();
		_inventory.catalyst = new Item();
		liquidInventory.Flush();
	}
	public void GiveResult(WitcheryRecipe.Result? result, Point16 tile, Player ply, TileEntity source) {
		if (result == null)
			return;
		foreach (var item in result.items)
			ply.QuickSpawnClonedItem(new EntitySource_TileEntity(source), item, item.stack);
		foreach (var liquid in result.liquids)
			liquidInventory.Add(liquid);
	}
}
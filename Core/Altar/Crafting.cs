using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TWitchery.AltarCore;
using PedestalCore;
using Liquids;
partial class Crafting {
	private List<WitcheryRecipe> _recipes;
	public readonly Inventory inventory;
	public List<Inventory> Satellies => new List<Inventory>();
	public Crafting(List<WitcheryRecipe> recipes = null) {
		_recipes = recipes == null ? new List<WitcheryRecipe>() : recipes;
		inventory = new Inventory();
	}

	public Action Interract(int i, int j, Player ply, Item[] inv, int slot) {
		// take item
		if (inventory.SlotsUsed > 0 && inv[slot].type == 0)
			return Action.Take;
		if (inv[slot].type == ModContent.ItemType<Items.EbonWand>())
			return Action.Craft;
		// put item
		return Action.Put;
	}
	// #nullable enable
	// public WitcheryRecipe.Result? Craft() {
	// 	var recipe = WitcheryRecipe.BestMatch(_recipes, inventory.slots, inventory.catalyst, liquidInventory.GetAll());
	// 	var result = recipe.Craft(inventory.slots, inventory.catalyst, liquidInventory.GetAll());

	// 	return result;
	// }
	public void Flush() {
		inventory.slots = Enumerable.Repeat(new Item(), inventory.slots.Length).ToArray();
	}
	public bool DrainEnergy(float amount, Player ply) {
		Main.NewText($"Mana: {ply.statMana + ply.GetModPlayer<TWitcheryPlayer>().CalcDepletionLimits()}/{amount}");

		var couldntTake = ply.GetModPlayer<TWitcheryPlayer>().TakeMana((int)amount, useDeplition: false);
		Main.NewText($"Mana: {ply.statMana + ply.GetModPlayer<TWitcheryPlayer>().CalcDepletionLimits()}/{amount}");
		return couldntTake <= 0;
	}
	public void GiveResult(WitcheryRecipe.Result result, Point16 tile, Player ply, TileEntity source) {
		if (result == null)
			return;
		foreach (var item in result.items)
			ply.QuickSpawnClonedItem(new EntitySource_TileEntity(source), item.self, item.self.stack);
	}
}
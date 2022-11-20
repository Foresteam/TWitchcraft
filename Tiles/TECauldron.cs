using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using TWitchery.Cauldron;

namespace TWitchery.Tiles;
using Liquids;
class TECauldron : TEAbstractStation, IRightClickable {
	private static List<WitcheryRecipe> _recipes = new List<WitcheryRecipe>(new WitcheryRecipe[] {
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Item(ItemID.DirtBlock, 5))
			.SetCatalyst(new Item(ItemID.Wood, 1))
			.AddResult(new Item(ItemID.StonePlatform, 10)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(1f))
			.AddIngredient(new Lava(1f))
			.AddResult(new Item(ItemID.Obsidian, 5)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Item(ItemID.Mushroom, 1))
			.AddIngredient(new Item(ItemID.Gel, 2))
			.AddIngredient(new Water(.5f))
			.AddResult(new WeakHealingPotion(.5f)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Item(ItemID.GlowingMushroom, 1))
			.AddIngredient(new WeakHealingPotion(.5f))
			.AddResult(new HealingPotion(.25f)),
	});
	private Crafting _crafting;
	public override Inventory Inventory => _crafting.inventory;
	public override LiquidInventory LiquidInventory => _crafting.liquidInventory;
	public TECauldron() {
		_crafting = new Crafting(5, 5f, _recipes);
	}

	public override bool IsValidTile(in Tile tile) => tile.TileType == ModContent.TileType<Cauldron>();
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
			case Crafting.Action.Take:
				_crafting.inventory.Take(i, j, ply);
				break;
			case Crafting.Action.Put:
				_crafting.inventory.Put(ref activeItem);
				break;
			case Crafting.Action.PutCatalyst:
				_crafting.inventory.PutCatalyst(ref activeItem);
				break;
			case Crafting.Action.Craft:
				var rs = _crafting.Craft();
				_crafting.GiveResult(rs, new Terraria.DataStructures.Point16(i, j), ply, this);
				break;
			case Crafting.Action.Pour: case Crafting.Action.Draw:
				_crafting.liquidInventory.Apply(ref inv[slot], ply);
				break;
			default:
				return false;
		}
		return true;
	}
}
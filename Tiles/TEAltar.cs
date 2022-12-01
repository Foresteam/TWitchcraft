using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using TWitchery.PedestalCore;
using TWitchery.AltarCore;

namespace TWitchery.Tiles;
class TEAltar : TEAbstractStation, IRightClickable {
	public const int inventorySize = 5;
	public const int radiusMin = 5, radiusMax = 6, pedestalDistance = radiusMax / 2 + 1;
	private static List<WitcheryRecipe> _recipes = new();
	private Crafting _crafting;
	public override Inventory Inventory => _crafting.inventory;
	public WorldItemDrawer ItemDrawer { get; private set; }
	public TEAltar() {
		_crafting = new Crafting();
		ItemDrawer = new WorldItemDrawer(() => Inventory.slots.First());
	}

	public override bool IsValidTile(in Tile tile) => tile.TileType == ModContent.TileType<Altar>();
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
				Inventory.Take(i, j, ply);
				break;
			case Crafting.Action.Put:
				Inventory.Put(ref activeItem);
				break;
			case Crafting.Action.Craft:
				Main.NewText(String.Join(", ", GetOverallInventory(i, j).Select(i => i.slots.First().Name)));
				break;
			default:
				return false;
		}
		return true;
	}

	public List<Inventory> GetOverallInventory(int i0, int j0) {
		HelpMe.GetTileTextureOrigin(ref i0, ref j0);
		List<Inventory> invs = new() { Inventory };
		List<Vector2> pedestals = new();
		for (int i = i0 - radiusMax; i < i0 + radiusMax; i++)
			for (int j = j0 - radiusMax; j < j0 + radiusMax; j++)
				if (Math.Sqrt(Math.Pow(i - i0, 2) + Math.Pow(j - j0, 2)) >= radiusMin) {
					var origin = HelpMe.GetTileTextureOrigin(new Terraria.DataStructures.Point16(i, j));
					var pedestal = HelpMe.GetTileEntity<TEPedestal>(i, j);
					if (pedestal == null || invs.Contains(pedestal.Inventory))
						continue;
					var altarPos = new Vector2(i0, j0);
					if (pedestals.FirstOrDefault(v => v.Distance(origin.ToVector2()) < pedestalDistance, altarPos) != altarPos)
						continue;
					pedestals.Add(origin.ToVector2());
					invs.Add(pedestal.Inventory);
				}

		return invs;
	}
}
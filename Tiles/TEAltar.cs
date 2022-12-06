using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

using TWitchery.PedestalCore;
using TWitchery.AltarCore;

namespace TWitchery.Tiles;
class TEAltar : TEAbstractStation, IRightClickable {
	public const int inventorySize = 5;
	public const int radiusMin = 5, radiusMax = 6, pedestalDistance = radiusMax / 2 + 1;
	private static List<WitcheryRecipe> _recipes = new() {
		new WitcheryRecipe(0)
			.AddIngredient(new Item(ItemID.DirtBlock, 5))
			.SetCatalyst(new Item(ItemID.Wood))
			.AddResult(new Item(ItemID.WoodenSword))
	};
	private Crafting _crafting;
	public override Inventory Inventory => _crafting.inventory;
	public WorldItemDrawer ItemDrawer { get; private set; }
	public TEAltar() {
		_crafting = new Crafting(_recipes);
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
				var rs = _crafting.Craft(GetSatelliteInventories(i, j));
				_crafting.Flush(GetOverallInventory(i, j));
				_crafting.GiveResult(rs, new Point16(i, j), ply, this);
				break;
			default:
				return false;
		}
		return true;
	}

	public List<Point16> GetPedestalsOrigins(int i0, int j0, out List<TEPedestal> pedestals) {
		HelpMe.GetTileTextureOrigin(ref i0, ref j0);
		List<Point16> origins = new();
		pedestals = new();
		for (int i = i0 - radiusMax; i < i0 + radiusMax; i++)
			for (int j = j0 - radiusMax; j < j0 + radiusMax; j++)
				if (Math.Sqrt(Math.Pow(i - i0, 2) + Math.Pow(j - j0, 2)) >= radiusMin) {
					var origin = HelpMe.GetTileTextureOrigin(new Terraria.DataStructures.Point16(i, j));
					var pedestal = HelpMe.GetTileEntity<TEPedestal>(i, j);
					if (pedestal == null || pedestals.Contains(pedestal))
						continue;
					var altarPos = new Point16(i0, j0);
					if (origins.FirstOrDefault(v => v.ToVector2().Distance(origin.ToVector2()) < pedestalDistance, altarPos) != altarPos)
						continue;
					origins.Add(origin);
					pedestals.Add(pedestal);
				}
		return origins;
	}
	public List<Point16> GetPedestalsOrigins(int i0, int j0) {
		List<TEPedestal> pedestals;
		return GetPedestalsOrigins(i0, j0, out pedestals);
	}
	public List<Inventory> GetSatelliteInventories(int i0, int j0) {
		List<TEPedestal> pedestals;
		GetPedestalsOrigins(i0, j0, out pedestals);
		return pedestals.Select(p => p.Inventory).ToList();
	}
	public List<Inventory> GetOverallInventory(int i0, int j0) {
		var inv = GetSatelliteInventories(i0, j0);
		inv.Insert(0, Inventory);
		return inv;
	}
}
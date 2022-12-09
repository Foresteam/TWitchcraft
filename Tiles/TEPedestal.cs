using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using TWitchery.PedestalCore;

namespace TWitchery.Tiles;
using Recipes;
#nullable enable
class TEPedestal : TEAbstractStation, IRightClickable {
	private static List<WitcheryRecipe> _recipes = new();
	public const int inventorySize = 5;
	private Inventory _inventory;
	public override Inventory Inventory => _inventory;
	public WorldItemDrawer ItemDrawer { get; private set; }
	public TEPedestal() {
		_inventory = new Inventory();
		ItemDrawer = new WorldItemDrawer(() => _inventory.slots.First());
	}

	public override bool IsValidTile(in Tile tile) => tile.TileType == ModContent.TileType<Pedestal>();
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
		switch (_inventory.Interract(i, j, ply, inv, slot)) {
			case PedestalCore.Action.Take:
				_inventory.Take(i, j, ply);
				break;
			case PedestalCore.Action.Put:
				_inventory.Put(ref activeItem);
				break;
			case PedestalCore.Action.ShowLink:
				var positions = LocateAltar(i, j);
				if (positions.Count > 0)
					Main.NewText($"Linked to: {String.Join(", ", positions)}");
				else
					Main.NewText("Not linked");
				break;
			default:
				return false;
		}
		return true;
	}
	/// <returns>Coordinates, if an altar is present. Null otherwise</returns>
	public List<Point16> LocateAltar(int i0, int j0) {
		HelpMe.GetTileTextureOrigin(ref i0, ref j0);
		var altars = new Dictionary<Point16, TEAltar>();
		for (int i = i0 - TEAltar.radiusMax - 4; i < i0 + TEAltar.radiusMax + 2; i++)
			for (int j = j0 - TEAltar.radiusMax - 4; j < j0 + TEAltar.radiusMax + 2; j++) {
				var altar = HelpMe.GetTileEntity<TEAltar>(i, j);
				if (altar != null) {
					var origin = HelpMe.GetTileTextureOrigin(new Point16(i, j));
					if (!altars.ContainsKey(origin))
						altars[origin] = altar;
				}
			}
		return altars.Keys.Where(origin => (bool)(TEAltar.GetSatelliteInventories(origin.X, origin.Y)?.Contains(Inventory) ?? false)).ToList();
	}
}
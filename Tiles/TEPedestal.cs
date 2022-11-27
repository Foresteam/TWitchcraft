using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

using TWitchery.Pedestal;

namespace TWitchery.Tiles;
using Tables;
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
		switch (_inventory.BasicInterract(i, j, ply, inv, slot)) {
			case StackedInventory.Action.Take:
				_inventory.Take(i, j, ply);
				break;
			case StackedInventory.Action.Put:
				_inventory.Put(ref activeItem);
				break;
			default:
				return false;
		}
		return true;
	}
}
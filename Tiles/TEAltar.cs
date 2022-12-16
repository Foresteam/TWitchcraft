using System;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

using TWitchery.AltarCore;
using TWitchery.PedestalCore;

#nullable enable
namespace TWitchery.Tiles;
using Recipes;
using Recipes.RecipeItems;
using static Tables.Vessels;
class TEAltar : TEAbstractStation, IBlockingRightClickable {
	public const int inventorySize = 5;
	public const int radiusMin = 5, radiusMax = 6, pedestalDistance = radiusMax / 2 + 1;
	private static List<WitcheryRecipe> _recipes = new() {
		new AltarRecipe(0)
			.AddIngredient(new Item(ItemID.DirtBlock, 5))
			.SetCatalyst(new Item(ItemID.Wood))
			.AddResult(new Item(ItemID.WoodenSword)),
		new AltarEnchantment(0)
			.AddIngredient(new Item(ItemID.DirtBlock))
			.SetTarget(new Item(ItemID.WoodenSword))
			.AddResult()
			.Enchant(5f),
		new AltarRecipe(800)
			.AddIngredient(new AnyGoldBar(10))
			.AddIngredient(Items.UniversalBottle.CreateFilled(new Liquids.Blood(), (int)(VolumeOf(ItemID.EmptyBucket) * 3 * (VolumeOf(ItemID.EmptyBucket) / VolumeOf(ItemID.Bottle)))))
			.SetTarget(new AnyEvilWood(60))
			.AddResult(new Item(ModContent.ItemType<Items.AdvancedEbonWand>()))
	};
	private Crafting _crafting;
	private Task? _craftingDelayTimer;
	public bool HighlightCrafted { get; private set; }
	public bool Combining => !_craftingDelayTimer?.IsCompleted ?? false;
	public override Inventory Inventory => _crafting.Inventory;
	public WorldItemDrawer ItemDrawer { get; private set; }
	public TEAltar() {
		_crafting = new Crafting(_recipes);
		_crafting.Inventory.ItemTaken += (item, slot) => HighlightCrafted = false;
		HighlightCrafted = false;

		ItemDrawer = new WorldItemDrawer(() => Inventory.Slot);
	}

	public override bool IsValidTile(in Tile tile) => tile.TileType == ModContent.TileType<Altar>();
	protected override void OnPlace(int i, int j) {
		// ass.Add(Main.rand.Next());
		Main.NewText("I exist, therefore i am in the world.");
	}
	public async Task<bool> RightClick(int i, int j) {
		var ply = Main.LocalPlayer;
		int slot = ply.selectedItem;
		var inv = ply.inventory;
		// no mouse yet
		switch (_crafting.Interract(i, j, ply, inv, slot)) {
			case Crafting.Action.Take:
				Inventory.Take(i, j, ply);
				break;
			case Crafting.Action.Put:
				Inventory.Put(ref inv[slot]);
				break;
			case Crafting.Action.Craft:
				_craftingDelayTimer = Task.Delay(5 * 1000);
				await _craftingDelayTimer;
				
				var rs = _crafting.Craft(i, j);
				if (rs != null)
					HighlightCrafted = true;
				_crafting.Flush(i, j);
				_crafting.GiveResult(rs, new Point16(i, j), ply, this);
				break;
			default:
				return false;
		}
		return true;
	}

	public static List<Point16>? GetPedestalsOrigins(int i0, int j0, out List<TEPedestal>? pedestals) {
		HelpMe.GetTileTextureOrigin(ref i0, ref j0);
		var altarPos = new Point16(i0, j0);
		List<Point16> origins = new();
		pedestals = new();
		if (HelpMe.GetTileEntity<TEAltar>(i0, j0) == null) {
			pedestals = null;
			return null;
		}
		for (int i = i0 - radiusMax; i < i0 + radiusMax; i++)
			for (int j = j0 - radiusMax; j < j0 + radiusMax; j++)
				if (Math.Sqrt(Math.Pow(i - i0, 2) + Math.Pow(j - j0, 2)) >= radiusMin) {
					var origin = HelpMe.GetTileTextureOrigin(new Terraria.DataStructures.Point16(i, j));
					var pedestal = HelpMe.GetTileEntity<TEPedestal>(i, j);
					if (pedestal == null || pedestals.Contains(pedestal))
						continue;
					
					if (origins.FirstOrDefault(v => v.ToVector2().Distance(origin.ToVector2()) < pedestalDistance, altarPos) != altarPos)
						continue;
					origins.Add(origin);
					pedestals.Add(pedestal);
				}
		return origins;
	}
	public static List<Point16>? GetPedestalsOrigins(int i0, int j0) {
		List<TEPedestal>? pedestals;
		return GetPedestalsOrigins(i0, j0, out pedestals);
	}
	public static List<Inventory>? GetSatelliteInventories(int i0, int j0) {
		List<TEPedestal>? pedestals;
		GetPedestalsOrigins(i0, j0, out pedestals);
		return pedestals?.Select(p => p.Inventory).ToList();
	}
	public static List<Inventory>? GetOverallInventory(int i0, int j0) {
		var inv = HelpMe.GetTileEntity<TEAltar>(i0, j0)?.Inventory;
		if (inv == null)
			return null;
		var invs = GetSatelliteInventories(i0, j0);
		invs?.Insert(0, inv);
		return invs;
	}
}
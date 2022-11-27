using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;

using TWitchery.Altar;
using TWitchery.Pedestal;

namespace TWitchery.Tiles;
using Tables;
class TEAltar : TEAbstractStation, IRightClickable {
	private static List<WitcheryRecipe> _recipes = new();
	public const int inventorySize = 5;
	private Crafting _crafting;
	public override Inventory Inventory => _crafting.inventory;
	public TEAltar() {
		_crafting = new Crafting(_recipes);
	}

	public override bool IsValidTile(in Tile tile) => tile.TileType == ModContent.TileType<Cauldron>();
	protected override void OnPlace(int i, int j) {
		// ass.Add(Main.rand.Next());
		Main.NewText("I exist, therefore i am in the world.");
	}
	private void CraftEffects(int i, int j, bool success) {
		HelpMe.GetTileOrigin(ref i, ref j);
		i++;
		var pos = new Vector2(i, j) * 16 + new Vector2(2, -8);
		var type = success ? DustID.MagicMirror : DustID.Smoke;
		var startVelocity = success ? -1f : -3f;
		for (int k = 0; k < 50; k++) {
			int dust = Dust.NewDust(new Vector2(i, j) * 16 + Cauldron.particleOrigin, 4, 4, type, 0, startVelocity, 100, default(Color), 1.5f);
			// Main.dust[dust].velocity.X *= .9f;
			if (success) {
				Main.dust[dust].velocity.X *= 1.7f;
				Main.dust[dust].velocity.Y *= 1.7f;
			}
			else
				Main.dust[dust].velocity.Y *= .7f;
			Main.dust[dust].noGravity = true;
		}
		if (success)
			SoundEngine.PlaySound(SoundID.Item29, pos);
		else
			SoundEngine.PlaySound(SoundID.LiquidsWaterLava, pos);
	}
	public bool RightClick(int i, int j) {
		var ply = Main.LocalPlayer;
		int slot = ply.selectedItem;
		var inv = ply.inventory;
		// no mouse yet
		ref var activeItem = ref inv[slot];
		// switch (_crafting.Interract(i, j, ply, inv, slot)) {
		// 	case Crafting.Action.Take:
		// 		_crafting.inventory.Take(i, j, ply);
		// 		break;
		// 	case Crafting.Action.Put:
		// 		AddItemEffects(i, j, activeItem.stack);
		// 		_crafting.inventory.Put(ref activeItem);
		// 		break;
		// 	case Crafting.Action.PutCatalyst:
		// 		AddItemEffects(i, j, activeItem.stack);
		// 		_crafting.inventory.PutCatalyst(ref activeItem);
		// 		break;
		// 	case Crafting.Action.Craft:
		// 		var rs = _crafting.Craft();
		// 		if (rs != null && !_crafting.DrainEnergy(rs.energyCost, _crafting.liquidInventory.GetAll(), ply)) {
		// 			Main.NewText("Not enough energy!", Color.Red);
		// 			// rs = null;
		// 			break;
		// 		}
		// 		if (rs != null && rs.liquids.Select(lq => lq.self.Volume).Sum() > _crafting.liquidInventory.volume) {
		// 			Main.NewText("Not enough space!", Color.Red);
		// 			break;
		// 		}
		// 		_crafting.Flush();
		// 		CraftEffects(i, j, rs != null);
		// 		_crafting.GiveResult(rs, new Terraria.DataStructures.Point16(i, j), ply, this);
		// 		break;
		// 	case Crafting.Action.Pour: case Crafting.Action.Draw:
		// 		_crafting.liquidInventory.Apply(ref inv[slot], ply);
		// 		break;
		// 	default:
		// 		return false;
		// }
		return true;
	}

	public static string DumpRecipes(bool dev) {
		var dump = _recipes.Select(recipe => recipe.Dump(dev)).ToList();
		dump.Insert(0, WitcheryRecipe.DumpHeader);
		return String.Join("\n", dump);
	}
}
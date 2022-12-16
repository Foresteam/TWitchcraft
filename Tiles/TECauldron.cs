using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using TWitchery.CauldronCore;

namespace TWitchery.Tiles;
using Liquids;
using Recipes;
#nullable enable
class TECauldron : TEAbstractStation, IRightClickable {
	public const int inventorySize = 5;
	private Crafting _crafting;
	private CauldronEnegyDrainer _energyDrainer;
	public override Inventory Inventory => _crafting.Inventory;
	public override LiquidInventory LiquidInventory => _crafting.liquidInventory;
	public TECauldron() {
		_crafting = new Crafting(inventorySize, 25f, Tables.CauldrounRecipes.self);
		_energyDrainer = new CauldronEnegyDrainer();
	}

	public override bool IsValidTile(in Tile tile) => tile.TileType == ModContent.TileType<Cauldron>();
	protected override void OnPlace(int i, int j) {
		// ass.Add(Main.rand.Next());
		Main.NewText("I exist, therefore i am in the world.");
	}
	private void CraftEffects(int i, int j, bool success) {
		HelpMe.GetTileTextureOrigin(ref i, ref j);
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
	private void AddItemEffects(int i, int j, int amount) {
		var color = Liquid.Blend(_crafting.liquidInventory.GetAll(), lq => lq.Color);
		if (color == null)
			return;
		HelpMe.GetTileTextureOrigin(ref i, ref j);
		i++;
		var pos = new Vector2(i, j) * 16 + Cauldron.particleOrigin;
		var type = ModContent.DustType<Dusts.Bubble>();
		for (int k = 0; k < Math.Min(100, amount * 2.5); k++) {
			int dust = Dust.NewDust(pos, 4, 4, type, 0, -2f, 100, (Color)color, .25f);
			Main.dust[dust].velocity.Y *= .3f;
			Main.dust[dust].velocity.X *= .6f;
			Main.dust[dust].noGravity = true;
		}
		SoundEngine.PlaySound(SoundID.Splash, pos);
	}
	public bool RightClick(int i, int j) {
		var ply = Main.LocalPlayer;
		int slot = ply.selectedItem;
		var inv = ply.inventory;
		// no mouse yet
		ref var activeItem = ref inv[slot];
		switch (_crafting.Interract(i, j, ply, inv, slot)) {
			case Crafting.Action.Take:
				_crafting.Inventory.Take(i, j, ply);
				break;
			case Crafting.Action.Put:
				AddItemEffects(i, j, activeItem.stack);
				_crafting.Inventory.Put(ref activeItem);
				break;
			case Crafting.Action.PutCatalyst:
				AddItemEffects(i, j, activeItem.stack);
				_crafting.Inventory.PutCatalyst(ref activeItem);
				break;
			case Crafting.Action.Craft:
				var rs = _crafting.Craft(i, j);
				HelpMe.ReduceEnergyCost(ref rs, ply, inv, slot);
				var ytd = _energyDrainer.Drain(rs?.energyCost ?? 0, ply, _crafting.liquidInventory);
				if (rs != null && ytd > 0) {
					Main.NewText($"Not enough energy! {ytd}", Color.Red);
					// rs = null;
					break;
				}
				if (rs != null && rs.liquids.Select(lq => lq.Volume).Sum() > _crafting.liquidInventory.volume) {
					Main.NewText("Not enough space!", Color.Red);
					break;
				}
				_crafting.Flush(rs, i, j);
				CraftEffects(i, j, rs != null);
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

	public static string DumpRecipes(bool dev) {
		var dump = Tables.CauldrounRecipes.self.Select(recipe => recipe.Dump(dev)).ToList();
		dump.Insert(0, WitcheryRecipe.DumpHeader);
		return String.Join("\n", dump);
	}
}
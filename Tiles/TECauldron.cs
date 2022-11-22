using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using TWitchery.Cauldron;

namespace TWitchery.Tiles;
using Liquids;
class TECauldron : TEAbstractStation, IRightClickable {
	private static Dictionary<int, float> VolumeOf = HelpMe.vesselsVolumes;
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
			.AddIngredient(new Item(ItemID.Gel, 2))
			.SetCatalyst(new Item(ItemID.Mushroom, 1))
			.AddIngredient(new Water(VolumeOf[ItemID.Bottle] * 2))
			.AddResult(new LesserHealingPotion(VolumeOf[ItemID.Bottle] * 2)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new LesserHealingPotion(VolumeOf[ItemID.Bottle] * 2))
			.SetCatalyst(new Item(ItemID.GlowingMushroom, 1))
			.AddResult(new HealingPotion(VolumeOf[ItemID.Bottle])),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(VolumeOf[ItemID.Bottle]))
			.AddIngredient(new Item(ItemID.CrystalShard))
			.AddResult(new LiquidCrystal(VolumeOf[ItemID.EmptyBucket] / 10f)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new LiquidCrystal(VolumeOf[ItemID.EmptyBucket] / 10f))
			.AddIngredient(new Water(VolumeOf[ItemID.Bottle] * 4f))
			.AddIngredient(new Item(ItemID.PixieDust, 3))
			.AddResult(new GreaterHealingPotion(VolumeOf[ItemID.Bottle] * 4f)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(VolumeOf[ItemID.Bottle] * 3f))
			.AddIngredient(new Item(ItemID.PixieDust, 3))
			.SetCatalyst(new Item(ItemID.CrystalShard))
			.AddResult(new GreaterHealingPotion(VolumeOf[ItemID.Bottle] * 3f)),

		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(VolumeOf[ItemID.EmptyBucket] * 1.5f))
			.SetCatalyst(new Item(ItemID.FallenStar, 1))
			.AddResult(new LesserManaPotion(VolumeOf[ItemID.EmptyBucket] * 1.5f)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new LesserManaPotion(VolumeOf[ItemID.Bottle] * 2))
			.SetCatalyst(new Item(ItemID.GlowingMushroom, 1))
			.AddResult(new ManaPotion(VolumeOf[ItemID.Bottle])),
		
		// The "secret" potion. Should this be used for something else?
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new HealingPotion(VolumeOf[ItemID.EmptyBucket]))
			.AddIngredient(new LesserHealingPotion(VolumeOf[ItemID.Bottle]))
			.AddIngredient(new ManaPotion(VolumeOf[ItemID.EmptyBucket]))
			.AddResult(new LifeforcePotion(VolumeOf[ItemID.Bottle])),

		new WitcheryRecipe(energyCost: 1.5f)
			.AddIngredient(new Item(ItemID.SandBlock, 2))
			.AddResult(new Glass(VolumeOf[ItemID.EmptyBucket] / 10f * 1.5f)),
		new WitcheryRecipe(energyCost: 1)
			.AddIngredient(new Item(ItemID.Glass))
			.AddResult(new Glass(VolumeOf[ItemID.EmptyBucket] / 10f)),
		new WitcheryRecipe(energyCost: 0, failedWorkedChance: 1f)
			.AddIngredient(new Glass(VolumeOf[ItemID.EmptyBucket] / 10f))
			.AddResult(new Item(ItemID.Glass)),
		new WitcheryRecipe(energyCost: 100)
			.AddIngredient(new LiquidCrystal(VolumeOf[ItemID.EmptyBucket]))
			.AddIngredient(new Item(ItemID.Glass, 5))
			.SetCatalyst(new Item(ItemID.ManaCrystal))
			// .AddIngredient(new Glass(VolumeOf[ItemID.EmptyBucket] / 2f))
			.AddResult(new Item(ItemID.CrystalBall))
	});
	private Crafting _crafting;
	public override Inventory Inventory => _crafting.inventory;
	public override LiquidInventory LiquidInventory => _crafting.liquidInventory;
	public TECauldron() {
		_crafting = new Crafting(5, 25f, _recipes);
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
	private void AddItemEffects(int i, int j, int amount) {
		var color = Liquid.Blend(_crafting.liquidInventory.GetAll(), lq => lq.Color);
		if (color == null)
			return;
		HelpMe.GetTileOrigin(ref i, ref j);
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
				_crafting.inventory.Take(i, j, ply);
				break;
			case Crafting.Action.Put:
				AddItemEffects(i, j, activeItem.stack);
				_crafting.inventory.Put(ref activeItem);
				break;
			case Crafting.Action.PutCatalyst:
				AddItemEffects(i, j, activeItem.stack);
				_crafting.inventory.PutCatalyst(ref activeItem);
				break;
			case Crafting.Action.Craft:
				var rs = _crafting.Craft();
				if (rs != null && !_crafting.DrainEnergy(rs.energyCost, _crafting.liquidInventory.GetAll(), ply)) {
					Main.NewText("Not enough energy!", Color.Red);
					// rs = null;
					break;
				}
				if (rs != null && rs.liquids.Select(lq => lq.self.Volume).Sum() > _crafting.liquidInventory.volume) {
					Main.NewText("Not enough space!", Color.Red);
					break;
				}
				_crafting.Flush();
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
}
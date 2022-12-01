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
using Tables;
class TECauldron : TEAbstractStation, IRightClickable {
	private static List<WitcheryRecipe> _recipes = new List<WitcheryRecipe>(new WitcheryRecipe[] {
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(1f))
			.AddIngredient(new Lava(1f))
			.AddResult(new Item(ItemID.Obsidian, 5)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.CrystalShard))
			.AddResult(new LiquidCrystal(Vessels.VolumeOf(ItemID.EmptyBucket) / 10f)),
			
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Item(ItemID.Gel, 2))
			.SetCatalyst(new Item(ItemID.Mushroom, 1))
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle) * 2))
			.AddResult(new LesserHealingPotion(Vessels.VolumeOf(ItemID.Bottle) * 2)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new LesserHealingPotion(Vessels.VolumeOf(ItemID.Bottle) * 2))
			.SetCatalyst(new Item(ItemID.GlowingMushroom, 1))
			.AddResult(new HealingPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new LiquidCrystal(Vessels.VolumeOf(ItemID.EmptyBucket) / 10f))
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle) * 4f))
			.AddIngredient(new Item(ItemID.PixieDust, 3))
			.AddResult(new GreaterHealingPotion(Vessels.VolumeOf(ItemID.Bottle) * 4f)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle) * 3f))
			.AddIngredient(new Item(ItemID.PixieDust, 3))
			.SetCatalyst(new Item(ItemID.CrystalShard))
			.AddResult(new GreaterHealingPotion(Vessels.VolumeOf(ItemID.Bottle) * 3f)),

		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.EmptyBucket) * 1.5f))
			.SetCatalyst(new Item(ItemID.FallenStar, 1))
			.AddResult(new LesserManaPotion(Vessels.VolumeOf(ItemID.EmptyBucket) * 1.5f)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new LesserManaPotion(Vessels.VolumeOf(ItemID.Bottle) * 2))
			.SetCatalyst(new Item(ItemID.GlowingMushroom, 1))
			.AddResult(new ManaPotion(Vessels.VolumeOf(ItemID.Bottle))),

		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new LesserHealingPotion(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.PinkGel))
			.SetCatalyst(new Item(ItemID.GlowingMushroom))
			.AddResult(new RestorationPotion(Vessels.VolumeOf(ItemID.Bottle))),
		
		// The "secret" potion. Should this be used for something else?
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new HealingPotion(Vessels.VolumeOf(ItemID.EmptyBucket)))
			.AddIngredient(new LesserHealingPotion(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new ManaPotion(Vessels.VolumeOf(ItemID.EmptyBucket)))
			.AddResult(new LifeforcePotion(Vessels.VolumeOf(ItemID.Bottle))),

		new WitcheryRecipe(energyCost: 1.5f)
			.AddIngredient(new Item(ItemID.SandBlock, 2))
			.AddResult(new Glass(Vessels.VolumeOf(ItemID.EmptyBucket) / 10f * 1.5f)),
		new WitcheryRecipe(energyCost: 1)
			.AddIngredient(new Item(ItemID.Glass))
			.AddResult(new Glass(Vessels.VolumeOf(ItemID.EmptyBucket) / 10f)),
		new WitcheryRecipe(energyCost: 0, failedWorkedChance: 1f)
			.AddIngredient(new Glass(Vessels.VolumeOf(ItemID.EmptyBucket) / 10f))
			.AddResult(new Item(ItemID.Glass)),
		new WitcheryRecipe(energyCost: 100)
			.AddIngredient(new LiquidCrystal(Vessels.VolumeOf(ItemID.EmptyBucket)))
			.AddIngredient(new Item(ItemID.Glass, 5))
			.SetCatalyst(new Item(ItemID.ManaCrystal))
			// .AddIngredient(new Glass(Vessels.GetVolume(ItemID.EmptyBucket) / 2f))
			.AddResult(new Item(ItemID.CrystalBall)),

		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Moonglow))
			.SetCatalyst(new Item(ItemID.DoubleCod))
			.AddResult(new AmmoReservationPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Daybloom))
			.SetCatalyst(new Item(ItemID.Lens))
			.AddResult(new ArcheryPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EvilExtract(.1f))
			.SetCatalyst(new Item(ItemID.Deathweed))
			.AddResult(new BattlePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Moonglow))
			.AddIngredient(new Item(ItemID.Shiverthorn))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new BattlePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Daybloom))
			.SetCatalyst(new Item(ItemID.Damselfish))
			.AddResult(new CalmingPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Moonglow))
			.AddIngredient(new Item(ItemID.Shiverthorn))
			.AddIngredient(new Item(ItemID.Waterleaf))
			.SetCatalyst(new Item(ItemID.Amber))
			.AddResult(new CratePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfAwareness(.1f))
			.SetCatalyst(new Item(ItemID.Shiverthorn))
			.AddResult(new DangersensePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Blinkroot))
			.SetCatalyst(new Item(ItemID.ArmoredCavefish))
			.AddResult(new EndurancePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AerialEssense(.1f))
			.AddIngredient(new Item(ItemID.Blinkroot))
			.SetCatalyst(new Item(ItemID.Daybloom))
			.AddResult(new FeatherfallPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Waterleaf))
			.SetCatalyst(new Item(ItemID.CrispyHoneyBlock))
			.AddResult(new FishingPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AquaticEssense(.1f))
			.AddIngredient(new Item(ItemID.Shiverthorn))
			.SetCatalyst(new Item(ItemID.Waterleaf))
			.AddResult(new FlipperPotion(Vessels.VolumeOf(ItemID.Bottle))),
		// new WitcheryRecipe(energyCost: 0)
		// 	.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
		// 	.AddIngredient(new Item(ItemID.Moonglow))
		// 	.AddIngredient(new Item(ItemID.Shiverthorn))
		// 	.AddIngredient(new Item(ItemID.Daybloom))
		// 	.AddIngredient(new Item(ItemID.Blinkroot))
		// 	.AddIngredient(new Item(ItemID.Fireblossom))
		// 	.AddIngredient(new Item(ItemID.Deathweed))
		// 	.SetCatalyst(new Item(ItemID.Waterleaf))
		// 	.AddResult(new GenderChangePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AquaticEssense(.1f))
			.AddIngredient(new Item(ItemID.Waterleaf))
			.SetCatalyst(new Item(ItemID.Coral))
			.AddResult(new GillsPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AlienEssense(.1f))
			.AddIngredient(new Item(ItemID.Blinkroot))
			.SetCatalyst(new Item(ItemID.Fireblossom))
			.AddResult(new GravitationPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new LifeEssense(.1f))
			.SetCatalyst(new Item(ItemID.Daybloom))
			.AddResult(new HeartreachPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfAwareness(.1f))
			.AddIngredient(new Item(ItemID.Daybloom))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new HunterPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Lava(.5f))
			.AddIngredient(new Item(ItemID.Fireblossom))
			.SetCatalyst(new Item(ItemID.FlarefinKoi))
			.AddResult(new InfernoPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Lava(Vessels.VolumeOf(ItemID.EmptyBucket) / 2f))
			.AddIngredient(new Item(ItemID.Fireblossom))
			.SetCatalyst(new Item(ItemID.Obsidifish))
			.AddResult(new InfernoPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfAwareness(.1f))
			.AddIngredient(new AlienEssense(.1f))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new InvisibilityPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(.1f))
			.SetCatalyst(new Item(ItemID.Daybloom))
			.AddResult(new IronskinPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Shiverthorn))
			.SetCatalyst(new Item(ItemID.PrincessFish))
			.AddResult(new LovePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(.1f))
			.AddIngredient(new AlienEssense(.1f))
			.SetCatalyst(new Item(ItemID.Moonglow))
			.AddResult(new MagicPowerPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(.1f))
			.AddIngredient(new LifeEssense(.1f))
			.SetCatalyst(new Item(ItemID.Moonglow))
			.AddResult(new MagicRestorationPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfDarkness(.1f))
			.AddIngredient(new EssenseOfForce(.1f))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new MiningPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfDarkness(.1f))
			.AddIngredient(new EssenseOfAwareness(.1f))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new NightOwlPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AlienEssense(.1f))
			.AddIngredient(new AquaticEssense(.1f))
			.AddIngredient(new Item(ItemID.Fireblossom))
			.SetCatalyst(new Item(ItemID.Obsidian))
			.AddResult(new ObsidianSkinPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(.1f))
			.SetCatalyst(new Item(ItemID.Hemopiranha))
			.AddResult(new RagePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Daybloom))
			.SetCatalyst(new Item(ItemID.SpecularFish))
			.AddResult(new RecallPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(.1f))
			.AddIngredient(new LifeEssense(.1f))
			.SetCatalyst(new Item(ItemID.Daybloom))
			.AddResult(new RegenerationPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new RecallPotion(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AlienEssense(.1f))
			.AddResult(new ReturnPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfDarkness(.1f))
			.AddIngredient(new Item(ItemID.Daybloom))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new ShinePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Coral))
			.SetCatalyst(new Item(ItemID.Waterleaf))
			.AddResult(new SonarPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfAwareness(.1f))
			.AddIngredient(new EssenseOfAvarice(.1f))
			.AddIngredient(new Item(ItemID.Moonglow))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new SpelunkerPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Deathweed))
			.SetCatalyst(new Item(ItemID.Stinkfish))
			.AddResult(new StinkPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AlienEssense(.1f))
			.AddIngredient(new EssenseOfForce(.1f))
			.SetCatalyst(new Item(ItemID.VariegatedLardfish))
			.AddResult(new SummoningPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Blinkroot))
			.SetCatalyst(new Item(ItemID.Cactus))
			.AddResult(new SwiftnessPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AlienEssense(.1f))
			.AddIngredient(new EssenseOfChaos(.1f))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new TeleportationPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(.1f))
			.AddIngredient(new Item(ItemID.AntlionMandible))
			.AddIngredient(new Item(ItemID.Stinger))
			.AddIngredient(new Item(ItemID.Deathweed))
			.SetCatalyst(new Item(ItemID.Cactus))
			.AddResult(new ThornsPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(.1f))
			.AddIngredient(new LifeEssense(.1f))
			.SetCatalyst(new Item(ItemID.Bone))
			.AddResult(new TitanPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Shiverthorn))
			.SetCatalyst(new Item(ItemID.FrostMinnow))
			.AddResult(new WarmthPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AquaticEssense(.1f))
			.AddIngredient(new AerialEssense(.1f))
			.SetCatalyst(new Item(ItemID.Waterleaf))
			.AddResult(new WaterWalkingPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfChaos(.1f))
			.AddIngredient(new EssenseOfOrder(.1f))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new WormholePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(.1f))
			.SetCatalyst(new Item(ItemID.Ebonkoi))
			.AddResult(new WrathPotion(Vessels.VolumeOf(ItemID.Bottle))),
	});
	public const int inventorySize = 5;
	private Crafting _crafting;
	private CauldronEnegyDrainer _energyDrainer;
	public override Inventory Inventory => _crafting.inventory;
	public override LiquidInventory LiquidInventory => _crafting.liquidInventory;
	public TECauldron() {
		_crafting = new Crafting(inventorySize, 25f, _recipes);
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
				if (rs != null && _energyDrainer.Drain(rs.energyCost, ply, _crafting.liquidInventory) > 0) {
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

	public static string DumpRecipes(bool dev) {
		var dump = _recipes.Select(recipe => recipe.Dump(dev)).ToList();
		dump.Insert(0, WitcheryRecipe.DumpHeader);
		return String.Join("\n", dump);
	}
}
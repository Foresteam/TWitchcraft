using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace TWitchery.Tables;
using Liquids;
using Recipes;
using Recipes.RecipeItems;
static class CauldrounRecipes {
	private const float _essenseDefaultRequirement = .1f;
	public static readonly List<WitcheryRecipe> self = new List<WitcheryRecipe>(new WitcheryRecipe[] {
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
			.AddIngredient(new LifeEssense(_essenseDefaultRequirement))
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
			.AddIngredient(new EvilExtract(_essenseDefaultRequirement))
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
			.AddIngredient(new EssenseOfAwareness(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Shiverthorn))
			.AddResult(new DangersensePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Blinkroot))
			.SetCatalyst(new Item(ItemID.ArmoredCavefish))
			.AddResult(new EndurancePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AerialEssense(_essenseDefaultRequirement))
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
			.AddIngredient(new AquaticEssense(_essenseDefaultRequirement))
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
			.AddIngredient(new AquaticEssense(_essenseDefaultRequirement))
			.AddIngredient(new Item(ItemID.Waterleaf))
			.SetCatalyst(new Item(ItemID.Coral))
			.AddResult(new GillsPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AlienEssense(_essenseDefaultRequirement))
			.AddIngredient(new Item(ItemID.Blinkroot))
			.SetCatalyst(new Item(ItemID.Fireblossom))
			.AddResult(new GravitationPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new LifeEssense(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Daybloom))
			.AddResult(new HeartreachPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfAwareness(_essenseDefaultRequirement))
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
			.AddIngredient(new EssenseOfAwareness(_essenseDefaultRequirement))
			.AddIngredient(new AlienEssense(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new InvisibilityPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Daybloom))
			.AddResult(new IronskinPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Shiverthorn))
			.SetCatalyst(new Item(ItemID.PrincessFish))
			.AddResult(new LovePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(_essenseDefaultRequirement))
			.AddIngredient(new AlienEssense(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Moonglow))
			.AddResult(new MagicPowerPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(_essenseDefaultRequirement))
			.AddIngredient(new LifeEssense(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Moonglow))
			.AddResult(new ManaRegenerationPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle) * 2))
			.AddIngredient(new EssenseOfDarkness(_essenseDefaultRequirement))
			.AddIngredient(new EssenseOfForce(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.AntlionMandible))
			.AddResult(new MiningPotion(Vessels.VolumeOf(ItemID.Bottle) * 2)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfDarkness(_essenseDefaultRequirement))
			.AddIngredient(new EssenseOfAwareness(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new NightOwlPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Lava(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AlienEssense(_essenseDefaultRequirement))
			.AddIngredient(new AquaticEssense(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Fireblossom))
			.AddResult(new ObsidianSkinPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Hemopiranha))
			.AddResult(new RagePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Daybloom))
			.SetCatalyst(new Item(ItemID.SpecularFish))
			.AddResult(new RecallPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(_essenseDefaultRequirement))
			.AddIngredient(new LifeEssense(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Daybloom))
			.AddResult(new RegenerationPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new RecallPotion(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AlienEssense(_essenseDefaultRequirement))
			.AddResult(new ReturnPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfDarkness(_essenseDefaultRequirement))
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
			.AddIngredient(new EssenseOfAwareness(_essenseDefaultRequirement))
			.AddIngredient(new EssenseOfAvarice(_essenseDefaultRequirement))
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
			.AddIngredient(new AlienEssense(_essenseDefaultRequirement))
			.AddIngredient(new EssenseOfForce(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.VariegatedLardfish))
			.AddResult(new SummoningPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Blinkroot))
			.SetCatalyst(new Item(ItemID.Cactus))
			.AddResult(new SwiftnessPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AlienEssense(_essenseDefaultRequirement))
			.AddIngredient(new EssenseOfChaos(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new TeleportationPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(_essenseDefaultRequirement))
			.AddIngredient(new Item(ItemID.AntlionMandible))
			.AddIngredient(new Item(ItemID.Stinger))
			.AddIngredient(new Item(ItemID.Deathweed))
			.SetCatalyst(new Item(ItemID.Cactus))
			.AddResult(new ThornsPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(_essenseDefaultRequirement))
			.AddIngredient(new LifeEssense(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Bone))
			.AddResult(new TitanPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.Shiverthorn))
			.SetCatalyst(new Item(ItemID.FrostMinnow))
			.AddResult(new WarmthPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AquaticEssense(_essenseDefaultRequirement))
			.AddIngredient(new AerialEssense(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Waterleaf))
			.AddResult(new WaterWalkingPotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfChaos(_essenseDefaultRequirement))
			.AddIngredient(new EssenseOfOrder(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Blinkroot))
			.AddResult(new WormholePotion(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new EssenseOfForce(_essenseDefaultRequirement))
			.SetCatalyst(new Item(ItemID.Ebonkoi))
			.AddResult(new WrathPotion(Vessels.VolumeOf(ItemID.Bottle))),

		new WitcheryRecipe(energyCost: 200)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.EmptyBucket)))
			.AddIngredient(new AnyMediumAnimal())
			.AddIngredient(new EssenseOfChaos(Vessels.VolumeOf(ItemID.Bottle)))
			.AddResult(new Blood(Vessels.VolumeOf(ItemID.EmptyBucket))),

		// here go extracts
		// the amount of energy required to extract bases on how supernatural the essense is?
		new WitcheryRecipe(energyCost: 20)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.EmptyBucket) * 2))
			.AddIngredient(new Item(ItemID.Daybloom))
			.AddIngredient(new Item(ItemID.Cobweb, 10))
			.SetCatalyst(new Item(ItemID.SharkFin, 1))
			.AddResult(new EssenseOfAwareness(Vessels.VolumeOf(ItemID.EmptyBucket) * 2)),
		new WitcheryRecipe(energyCost: 40)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.EmptyBucket)))
			.AddIngredient(new Item(ItemID.Torch, 5))
			.SetCatalyst(new Item(ItemID.Obsidian))
			.AddResult(new EssenseOfDarkness(Vessels.VolumeOf(ItemID.Bottle) * 2)),
		new WitcheryRecipe(energyCost: 50)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.EmptyBucket)))
			.AddIngredient(new Item(ItemID.Feather, 3))
			.AddIngredient(new Item(ItemID.RainCloud))
			.AddIngredient(new Item(ItemID.Cloud, 4))
			.SetCatalyst(new AnyGoldOre())
			.AddResult(new AerialEssense(Vessels.VolumeOf(ItemID.Bottle) * 2)),
		new WitcheryRecipe(energyCost: 0)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.SetCatalyst(new Item(ItemID.SoulofFlight))
			.AddResult(new AerialEssense(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 20)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new AnyEvilBlock(5))
			.AddResult(new EvilExtract(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 80)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.EmptyBucket)))
			.AddIngredient(new Item(ItemID.GrayBrick, 5))
			.AddIngredient(new Item(ItemID.MarbleBlock, 2))
			.AddResult(new EssenseOfOrder(Vessels.VolumeOf(ItemID.Bottle) * 1)),
		new WitcheryRecipe(energyCost: 160)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.EmptyBucket) * 4))
			.AddIngredient(new Item(ItemID.Glowstick, 4))
			.AddIngredient(new Item(ItemID.SharkFin, 1))
			.AddIngredient(new Item(ItemID.Coral, 5))
			.SetCatalyst(new Item(ItemID.Waterleaf, 1))
			.AddResult(new AquaticEssense(Vessels.VolumeOf(ItemID.Bottle) * 2)),

		new WitcheryRecipe(energyCost: 40)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.EmptyBucket)))
			.AddIngredient(new EvilExtract(Vessels.VolumeOf(ItemID.Bottle) * 2))
			.SetCatalyst(new Item(ItemID.Deathweed))
			.AddResult(new EssenseOfChaos(Vessels.VolumeOf(ItemID.Bottle) * 2)),

		new WitcheryRecipe(energyCost: 40)
			.AddIngredient(new Water(Vessels.VolumeOf(ItemID.Bottle) * 3))
			.AddIngredient(new EssenseOfChaos(Vessels.VolumeOf(ItemID.Bottle)))
			.AddIngredient(new Item(ItemID.FallenStar))
			.SetCatalyst(new AnyIronOre(3))
			.AddResult(new EssenseOfForce(Vessels.VolumeOf(ItemID.Bottle) * 1)),
		new WitcheryRecipe(energyCost: 60)
			.AddIngredient(new Blood(Vessels.VolumeOf(ItemID.Bottle) * 2))
			.AddIngredient(new EssenseOfOrder(Vessels.VolumeOf(ItemID.Bottle)))
			.SetCatalyst(new Item(ItemID.Wood, 5))
			.AddResult(new LifeEssense(Vessels.VolumeOf(ItemID.Bottle))),
		new WitcheryRecipe(energyCost: 20)
			.AddIngredient(new EssenseOfDarkness(Vessels.VolumeOf(ItemID.Bottle) * 2))
			.SetCatalyst(new AnyGoldOre())
			.AddResult(new EssenseOfAvarice(Vessels.VolumeOf(ItemID.Bottle) * 2)),
		new WitcheryRecipe(energyCost: 50)
			.AddIngredient(new AerialEssense(Vessels.VolumeOf(ItemID.Bottle) * 2))
			.AddIngredient(new EssenseOfDarkness(Vessels.VolumeOf(ItemID.Bottle) * 2))
			.AddIngredient(new EssenseOfChaos(Vessels.VolumeOf(ItemID.Bottle) * 1))
			.SetCatalyst(new AnyGoldOre())
			.AddResult(new AlienEssense(Vessels.VolumeOf(ItemID.Bottle) * 4)),
	});
}
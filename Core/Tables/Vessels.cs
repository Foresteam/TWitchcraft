using System;
using System.Linq;
using System.Collections.Generic;
using Terraria.ID;

namespace TWitchery.Tables;
using Liquids;
#nullable enable
class Vessels {
	/// <summary>FilledVessel to Liquid pairs (volume included). A factory?</summary>
	public static readonly Dictionary<int, Func<float, Liquid>> vesselsLiquids = new() {
		{ 0, volume => null },
		{ ItemID.WaterBucket, volume => new Water(volume) },
		{ ItemID.BottledWater, volume => new Water(volume) },
		{ ItemID.LavaBucket, volume => new Lava(volume) },

		{ ItemID.LesserHealingPotion, volume => new LesserHealingPotion(volume) },
		{ ItemID.HealingPotion, volume => new HealingPotion(volume) },
		{ ItemID.GreaterHealingPotion, volume => new GreaterHealingPotion(volume) },

		{ ItemID.LesserManaPotion, volume => new LesserManaPotion(volume) },
		{ ItemID.ManaPotion, volume => new ManaPotion(volume) },

		{ ItemID.RestorationPotion, volume => new RestorationPotion(volume) },

		{ ItemID.LifeforcePotion, volume => new LifeforcePotion(volume) },
		//
		{ ItemID.AmmoReservationPotion, volume => new AmmoReservationPotion(volume) },
		{ ItemID.ArcheryPotion, volume => new ArcheryPotion(volume) },
		{ ItemID.BattlePotion, volume => new BattlePotion(volume) },
		{ ItemID.BuilderPotion, volume => new BuilderPotion(volume) },
		{ ItemID.CalmingPotion, volume => new CalmingPotion(volume) },
		{ ItemID.CratePotion, volume => new CratePotion(volume) },
		{ ItemID.TrapsightPotion, volume => new DangersensePotion(volume) },
		{ ItemID.EndurancePotion, volume => new EndurancePotion(volume) },
		{ ItemID.FeatherfallPotion, volume => new FeatherfallPotion(volume) },
		{ ItemID.FishingPotion, volume => new FishingPotion(volume) },
		{ ItemID.FlipperPotion, volume => new FlipperPotion(volume) },
		{ ItemID.GenderChangePotion, volume => new GenderChangePotion(volume) },
		{ ItemID.GillsPotion, volume => new GillsPotion(volume) },
		{ ItemID.GravitationPotion, volume => new GravitationPotion(volume) },
		{ ItemID.LuckPotionGreater, volume => new GreaterLuckPotion(volume) },
		{ ItemID.HeartreachPotion, volume => new HeartreachPotion(volume) },
		{ ItemID.HunterPotion, volume => new HunterPotion(volume) },
		{ ItemID.InfernoPotion, volume => new InfernoPotion(volume) },
		{ ItemID.InvisibilityPotion, volume => new InvisibilityPotion(volume) },
		{ ItemID.IronskinPotion, volume => new IronskinPotion(volume) },
		{ ItemID.LuckPotionLesser, volume => new LesserLuckPotion(volume) },
		{ ItemID.LovePotion, volume => new LovePotion(volume) },
		{ ItemID.LuckPotion, volume => new LuckPotion(volume) },
		{ ItemID.MagicPowerPotion, volume => new MagicPowerPotion(volume) },
		{ ItemID.ManaRegenerationPotion, volume => new MagicRestorationPotion(volume) },
		{ ItemID.MiningPotion, volume => new MiningPotion(volume) },
		{ ItemID.NightOwlPotion, volume => new NightOwlPotion(volume) },
		{ ItemID.ObsidianSkinPotion, volume => new ObsidianSkinPotion(volume) },
		{ ItemID.RagePotion, volume => new RagePotion(volume) },
		{ ItemID.RecallPotion, volume => new RecallPotion(volume) },
		{ ItemID.RegenerationPotion, volume => new RegenerationPotion(volume) },
		{ ItemID.PotionOfReturn, volume => new ReturnPotion(volume) },
		{ ItemID.ShinePotion, volume => new ShinePotion(volume) },
		{ ItemID.SonarPotion, volume => new SonarPotion(volume) },
		{ ItemID.SpelunkerPotion, volume => new SpelunkerPotion(volume) },
		{ ItemID.StinkPotion, volume => new StinkPotion(volume) },
		{ ItemID.SummoningPotion, volume => new SummoningPotion(volume) },
		{ ItemID.SwiftnessPotion, volume => new SwiftnessPotion(volume) },
		{ ItemID.TeleportationPotion, volume => new TeleportationPotion(volume) },
		{ ItemID.ThornsPotion, volume => new ThornsPotion(volume) },
		{ ItemID.TitanPotion, volume => new TitanPotion(volume) },
		{ ItemID.WarmthPotion, volume => new WarmthPotion(volume) },
		{ ItemID.WaterWalkingPotion, volume => new WaterWalkingPotion(volume) },
		{ ItemID.WormholePotion, volume => new WormholePotion(volume) },
		{ ItemID.WrathPotion, volume => new WrathPotion(volume) },
	};
	public static readonly Dictionary<int, float> vesselsVolumes = new() {
		{ ItemID.EmptyBucket, 1f },
		{ ItemID.Bottle, .25f },
	};
	/// <summary>FilledVessel => EmptyVessel pairs</summary>
	public static readonly Dictionary<int, int> vessels = new() {
		{ ItemID.EmptyBucket, ItemID.EmptyBucket },
		{ ItemID.WaterBucket, ItemID.EmptyBucket },
		{ ItemID.LavaBucket, ItemID.EmptyBucket },
		{ ItemID.HoneyBucket, ItemID.EmptyBucket },
		{ ItemID.ChumBucket, ItemID.EmptyBucket },

		{ ItemID.Bottle, ItemID.Bottle },
		{ ItemID.BottledHoney, ItemID.Bottle },
		{ ItemID.BottledWater, ItemID.Bottle },
		{ ItemID.LesserHealingPotion, ItemID.Bottle },
		{ ItemID.HealingPotion, ItemID.Bottle },
		{ ItemID.GreaterHealingPotion, ItemID.Bottle },
		{ ItemID.LesserManaPotion, ItemID.Bottle },
		{ ItemID.ManaPotion, ItemID.Bottle },
		{ ItemID.RestorationPotion, ItemID.Bottle },

		{ ItemID.LifeforcePotion, ItemID.Bottle },
		{ ItemID.AmmoReservationPotion, ItemID.Bottle },
		{ ItemID.ArcheryPotion, ItemID.Bottle },
		{ ItemID.BattlePotion, ItemID.Bottle },
		{ ItemID.BuilderPotion, ItemID.Bottle },
		{ ItemID.CalmingPotion, ItemID.Bottle },
		{ ItemID.CratePotion, ItemID.Bottle },
		{ ItemID.TrapsightPotion, ItemID.Bottle },
		{ ItemID.EndurancePotion, ItemID.Bottle },
		{ ItemID.FeatherfallPotion, ItemID.Bottle },
		{ ItemID.FishingPotion, ItemID.Bottle },
		{ ItemID.FlipperPotion, ItemID.Bottle },
		{ ItemID.GenderChangePotion, ItemID.Bottle },
		{ ItemID.GillsPotion, ItemID.Bottle },
		{ ItemID.GravitationPotion, ItemID.Bottle },
		{ ItemID.LuckPotionGreater, ItemID.Bottle },
		{ ItemID.HeartreachPotion, ItemID.Bottle },
		{ ItemID.HunterPotion, ItemID.Bottle },
		{ ItemID.InfernoPotion, ItemID.Bottle },
		{ ItemID.InvisibilityPotion, ItemID.Bottle },
		{ ItemID.IronskinPotion, ItemID.Bottle },
		{ ItemID.LuckPotionLesser, ItemID.Bottle },
		{ ItemID.LovePotion, ItemID.Bottle },
		{ ItemID.LuckPotion, ItemID.Bottle },
		{ ItemID.MagicPowerPotion, ItemID.Bottle },
		{ ItemID.ManaRegenerationPotion, ItemID.Bottle },
		{ ItemID.MiningPotion, ItemID.Bottle },
		{ ItemID.NightOwlPotion, ItemID.Bottle },
		{ ItemID.ObsidianSkinPotion, ItemID.Bottle },
		{ ItemID.RagePotion, ItemID.Bottle },
		{ ItemID.RecallPotion, ItemID.Bottle },
		{ ItemID.RegenerationPotion, ItemID.Bottle },
		{ ItemID.PotionOfReturn, ItemID.Bottle },
		{ ItemID.ShinePotion, ItemID.Bottle },
		{ ItemID.SonarPotion, ItemID.Bottle },
		{ ItemID.SpelunkerPotion, ItemID.Bottle },
		{ ItemID.StinkPotion, ItemID.Bottle },
		{ ItemID.SummoningPotion, ItemID.Bottle },
		{ ItemID.SwiftnessPotion, ItemID.Bottle },
		{ ItemID.TeleportationPotion, ItemID.Bottle },
		{ ItemID.ThornsPotion, ItemID.Bottle },
		{ ItemID.TitanPotion, ItemID.Bottle },
		{ ItemID.WarmthPotion, ItemID.Bottle },
		{ ItemID.WaterWalkingPotion, ItemID.Bottle },
		{ ItemID.WormholePotion, ItemID.Bottle },
		{ ItemID.WrathPotion, ItemID.Bottle },
	};

	public static bool IsVessel(int itemID) => vessels.ContainsKey(itemID);

	public static float VolumeOf(int itemID) {
		if (!IsVessel(itemID))
			throw new ArgumentException("The item is not a vessel");
		return vesselsVolumes[vessels[itemID]];
	}
	public static float GetVolume(Terraria.Item item) => VolumeOf(item.type);
}
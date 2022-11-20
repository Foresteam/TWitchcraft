using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Terraria.ID;
using TWitchery.Liquids;

namespace TWitchery;
static partial class HelpMe {
	/// <summary>FilledVessel to Liquid pairs (volume included)</summary>
	public static readonly Dictionary<int, Func<float, Liquid>> vesselsLiquids = new() {
		{ 0, (float volume) => null },
		{ ItemID.WaterBucket, (float volume) => new Water(volume) },
		{ ItemID.BottledWater, (float volume) => new Water(volume) },
		{ ItemID.LavaBucket, (float volume) => new Lava(volume) },
		{ ItemID.LesserHealingPotion, (float volume) => new WeakHealingPotion(volume) },
		{ ItemID.HealingPotion, (float volume) => new HealingPotion(volume) },
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
	};

	public static class Vessel
	{
		public static bool IsVessel(int itemID) => vessels.ContainsKey(itemID);
		public static bool IsVessel(Terraria.Item item) => IsVessel(item.type);
		public static bool IsEmpty(int itemID) => IsVessel(itemID) && vessels[itemID] == itemID;
		public static bool IsEmpty(Terraria.Item item) => IsEmpty(item.type);

		public static bool IsFilled(int itemID) => IsVessel(itemID) && !IsEmpty(itemID);
		public static bool IsFilled(Terraria.Item item) => IsFilled(item.type);


		/// <summary>Get "filled" vessel for the liquid</summary>
		/// <returns>ItemID if a suitable one was found, 0 otherwise</returns>
#nullable enable
		public static int GetFilledWith(int itemID, Liquid? liquid) {
			if (!IsEmpty(itemID) || liquid == null)
				return 0;
			return vesselsLiquids.FirstOrDefault(
				pair => pair.Key != 0 && pair.Value(GetVolume(itemID)).GetType() == liquid.GetType() && vessels[pair.Key] == itemID,
				vesselsLiquids.First()
			).Key;
		}
		public static int GetFilledWith(Terraria.Item item, Liquid liquid) => GetFilledWith(item.type, liquid);
		public static float GetVolume(int itemID) {
			if (!IsVessel(itemID))
				throw new ArgumentException("The item is not a vessel");
			return vesselsVolumes[vessels[itemID]];
		}
		public static float GetVolume(Terraria.Item item) => GetVolume(item.type);
	}
}
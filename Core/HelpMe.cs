using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.DataStructures;
using TWitchery.Liquids;

namespace TWitchery;
static class HelpMe {
	/// <summary>FilledVessel to Liquid pairs (volume included)</summary>
	public static readonly Dictionary<int, Func<float, Liquid>> vesselsLiquids = new() {
		{ ItemID.WaterBucket, (float volume) => new Water(volume) },
		{ ItemID.BottledWater, (float volume) => new Water(volume) },
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
	};

	public static class Vessel {
		public static bool IsVessel(int itemID) => vessels.ContainsKey(itemID);
		public static bool IsVessel(Terraria.Item item) => IsVessel(item.type);
		public static bool IsEmpty(int itemID) => IsVessel(itemID) && vessels[itemID] == itemID;
		public static bool IsEmpty(Terraria.Item item) => IsEmpty(item.type);

		public static bool IsFilled(int itemID) => IsVessel(itemID) && !IsEmpty(itemID);
		public static bool IsFilled(Terraria.Item item) => IsFilled(item.type);


		/// <summary>Get "filled" vessel for the liquid</summary>
		/// <returns>ItemID if a suitable one was found, -1 otherwise</returns>
		public static int GetFilledWith(int itemID, Liquid liquid) {
			if (!IsEmpty(itemID))
				return -1;

			return vesselsLiquids.First(pair => pair.Value(GetVolume(itemID)).GetType() == liquid.GetType() && vessels[pair.Key] == itemID).Key;
		}
		public static int GetFilledWith(Terraria.Item item, Liquid liquid) => GetFilledWith(item.type, liquid);
		public static float GetVolume(int itemID) {
			if (!IsVessel(itemID))
				throw new ArgumentException("The item is not a vessel");
			return vesselsVolumes[vessels[itemID]];
		}
		public static float GetVolume(Terraria.Item item) => GetVolume(item.type);
	}

	public static void GetTileOrigin(ref int i, ref int j) {
		i -= Terraria.Main.tile[i, j].TileFrameX / 16;
		j -= -1 + Terraria.Main.tile[i, j].TileFrameY / 16;
	}
	public static Point16 GetTileOrigin(Point16 point) {
		int i = point.X, j = point.Y;
		GetTileOrigin(ref i, ref j);
		return new Point16(i, j);
	}
	public static T GetTileEntity<T>(int i, int j) where T: TileEntity {
		GetTileOrigin(ref i, ref j);

		TileEntity te;
		if (!TileEntity.ByPosition.TryGetValue(new Point16(i, j), out te) || te is not T)
			return null;
		return (T)te;
	}
	public static bool Consume(ref Terraria.Item item, int amount = 1) {
		if (item.stack < amount)
			return false;
		if (item.stack > amount)
			item.stack -= amount;
		else
			item = new Terraria.Item();
		return true;
	}
	public static void GiveItem(Terraria.Item item, Terraria.Player ply, int pid = -1) {
		if (pid == -1)
			pid = Terraria.Main.myPlayer;
		Terraria.Item taken = ply.GetItem(pid, item, Terraria.GetItemSettings.InventoryEntityToPlayerInventorySettings);
		if (!taken.IsAir)
			ply.QuickSpawnItem(null, taken);
	}
	public static void Swap<T>(ref T a, ref T b) {
		var t = a;
		a = b;
		b = t;
	}
}
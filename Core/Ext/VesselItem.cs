using System.Linq;
using Terraria;

namespace TWitchery.ExtVesselItem;
using Tables;
using Liquids;
#nullable enable
static class VesselItemExt {
	public static bool IsVessel(this Item item) => Vessels.vessels.ContainsKey(item.type);
	public static bool IsEmpty(this Item item) {
		if (item.type != Terraria.ModLoader.ModContent.ItemType<Items.UniversalBottle>())
			return item.IsVessel() && Vessels.vessels[item.type] == item.type;
		return item.IsVessel() && (item.ModItem as Items.UniversalBottle)?.storedLiquid == null;
	}
	public static int GetEmpty(this Item item) => Tables.Vessels.vessels.ContainsKey(item.type) ? Tables.Vessels.vessels[item.type] : 0;
	public static bool IsFilled(this Item item) => item.IsVessel() && !item.IsEmpty();
	public static float GetVolume(this Item item) => Vessels.VolumeOf(item.type);
	/// <summary>Get "filled" vessel for the liquid</summary>
	/// <returns>ItemID if a suitable one was found, 0 otherwise</returns>
	public static int GetFilledWith(this Item item, Liquid? liquid) {
		if (!item.IsEmpty() || liquid == null)
			return 0;
		return Vessels.vesselsLiquids.FirstOrDefault(
			pair => pair.Key != 0 && pair.Value(item.GetVolume())?.GetType() == liquid.GetType() && Vessels.vessels[pair.Key] == item.type,
			Vessels.vesselsLiquids.First()
		).Key;
	}
}
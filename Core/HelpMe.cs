using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace TWitchery;
static partial class HelpMe {
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

	/// <summary>Blends the specified colors together.</summary>
	/// <param name="color">Color to blend onto the background color.</param>
	/// <param name="backColor">Color to blend the other color onto.</param>
	/// <param name="amount">How much of <paramref name="color"/> to keep,
	/// “on top of” <paramref name="backColor"/>.</param>
	/// <returns>The blended colors.</returns>
	public static Color Blend(this Color color, Color backColor, float amount1, float amount2 = 0) {
		if (amount2 == 0)
			amount2 = 1 - amount1;
		byte r = (byte)(color.R * amount1 + backColor.R * amount2);
		byte g = (byte)(color.G * amount1 + backColor.G * amount2);
		byte b = (byte)(color.B * amount1 + backColor.B * amount2);
		return new Color(r, g, b);
	}
}
using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;

namespace TWitchery;
static partial class HelpMe {
	// f'd up. Fix this
	public static void GetTileTextureOrigin(ref int i, ref int j) {
		i -= Terraria.Main.tile[i, j].TileFrameX / 16;
		j -= Terraria.Main.tile[i, j].TileFrameY / 16;
	}
	public static Point16 GetTileTextureOrigin(Point16 point) {
		int i = point.X, j = point.Y;
		GetTileTextureOrigin(ref i, ref j);
		return new Point16(i, j);
	}
	public static T GetTileEntity<T>(int i, int j) where T: TileEntity {
		GetTileTextureOrigin(ref i, ref j);

		TileEntity te;
		if (!TileEntity.ByPosition.TryGetValue(GetTileTextureOrigin(new Point16(i, j)), out te) || te is not T)
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
	/// <param name="color">First color</param>
	/// <param name="color2">Second color</param>
	/// <param name="k1">Amount of <paramref name="color"/>, supposed to be less or equal than 1</param>
	/// <param name="k2">Amount of <paramref name="color2"/>, supposed to be less or equal than 1</param>
	/// <returns>The blended colors.</returns>
	public static Color Blend(Color color, Color color2, float k1, float k2 = 0) {
		if (k2 == 0)
			k2 = 1 - k1;
		byte r = (byte)(color.R * k1 + color2.R * k2);
		byte g = (byte)(color.G * k1 + color2.G * k2);
		byte b = (byte)(color.B * k1 + color2.B * k2);
		return new Color(r, g, b);
	}

	public static string DumpItem(Item item, bool dev = false) {
		if (!dev)
			return $"{item.Name} x{item.stack}";
		else
			return '{' + $"\"id\": {item.type}, \"stack\": {item.stack}" + '}';
	}
}
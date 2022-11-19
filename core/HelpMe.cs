using Terraria;
using Terraria.DataStructures;

namespace TWitchery {
	public static class HelpMe {
		public static void GetTileOrigin(ref int i, ref int j) {
			i -= Main.tile[i, j].TileFrameX / 16;
			j -= -1 + Main.tile[i, j].TileFrameY / 16;
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
		public static void Swap<T>(ref T a, ref T b) {
			var t = a;
			a = b;
			b = t;
		}
	}
}
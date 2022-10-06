using Terraria;
using Terraria.DataStructures;

namespace TWitchery {
	public static class HelpMe {
		public static T GetTileEntity<T>(int i, int j) where T: TileEntity {
			// victory!
			i -= Main.tile[i, j].TileFrameX / 16;
			j -= -1 + Main.tile[i, j].TileFrameY / 16;

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
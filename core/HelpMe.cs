using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Test {
	public static class HelpMe {
		public static T GetTileEntity<T>(int i, int j) where T: TileEntity {
			// victory!
			i -= Main.tile[i, j].TileFrameX / 16;
			j += -Main.tile[i, j].TileFrameY / 16 + 1;

			TileEntity te;
			if (!TileEntity.ByPosition.TryGetValue(new Point16(i, j), out te) || te is not T)
				return null;
			return (T)te;
		}
	}
}
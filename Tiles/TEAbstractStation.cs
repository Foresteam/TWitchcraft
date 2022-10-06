using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TWitchery.Tiles {
	public abstract class TEAbstractStation : ModTileEntity {
		public virtual bool IsValidTile(in Tile tile) => false;
		protected virtual void OnPlace(int i, int j) {}

		public override bool IsTileValidForEntity(int x, int y)	{
			Tile tile = Main.tile[x, y];
			return tile.HasTile && IsValidTile(tile);
		}
		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate) {
			if (Main.netMode == NetmodeID.MultiplayerClient) {
				// NetHelper.SendComponentPlace(i - 1, j - 1, Type);
				return -1;
			}

			int id = Place(i - 1, j - 1);
			((TEAbstractStation)ByID[id]).OnPlace(i, j);
			return id;
		}
	}
}
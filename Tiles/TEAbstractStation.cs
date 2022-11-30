using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TWitchery.Tiles;
abstract class TEAbstractStation : ModTileEntity {
	public virtual StackedInventory Inventory => null;
	public virtual LiquidInventory LiquidInventory => null;
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

		HelpMe.GetTileTextureOrigin(ref i, ref j);
		int id = Place(i, j);
		((TEAbstractStation)ByID[id]).OnPlace(i, j);
		return id;
	}
}
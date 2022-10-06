using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Test.Tiles {
	public class CauldronTE : ModTileEntity {
		private int _uid;

		private bool IsValidTile(in Tile tile) => tile.TileType == ModContent.TileType<TestCauldron>();
		public override bool IsTileValidForEntity(int x, int y) {
			Tile tile = Main.tile[x, y];
			return tile.HasTile && IsValidTile(tile);
		}
		private void OnPlace() {
			_uid = Main.rand.Next();
			Main.NewText("I exist, therefore i am in the world." + _uid);
		}
		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate) {
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				// NetHelper.SendComponentPlace(i - 1, j - 1, Type);
				return -1;
			}

			int id = Place(i - 1, j - 1);
			((CauldronTE)ByID[id]).OnPlace();
			// ((TEStorageComponent)ByID[id]).OnPlace();
			return id;
		}
		public void PrintMyNumber() {
			Main.NewText(_uid);
		}
	}
}
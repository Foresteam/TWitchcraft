using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Test.Tiles {
	public class TestCauldron : ModTile {
		private WitcheryCrafting _crafting;

		// public TestCauldron() {
		// }
		public override void PlaceInWorld(int i, int j, Item tileItem) {
			_crafting = new WitcheryCrafting(5, false);
		}
		public override void SetStaticDefaults() {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = false;
            Main.tileLavaDeath[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			// tile heights, from top to bottom. Or not?..
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 12 };
			// IMPORTANT TO STATE
			TileObjectData.newTile.CoordinatePadding = 0;
			// For some reason, it's to be
			TileObjectData.newTile.DrawYOffset = 8;
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault(Items.Placeables.TestCauldron.DisplayNameText);
            AddMapEntry(new Color(200, 200, 200), name);           
            // disableSmartCursor = true;
            AdjTiles = new int[] { TileID.CookingPots };
            MinPick = 0;
            Main.tileCut[Type] = false;
        }
		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
			Item.NewItem(null, i * 16, j * 16, 32, 16, ModContent.ItemType<Items.Placeables.TestCauldron>());
		}
		public override bool RightClick(int i, int j) {
			var ply = Main.LocalPlayer;
			int slot = ply.selectedItem;
			var inv = ply.inventory;
			_crafting.RMBInterract(i, j, ply, inv, slot);
			return true;
		}
	}
}
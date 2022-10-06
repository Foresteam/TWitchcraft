using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TWitchery.Tiles {
	public class TestCauldron : ModTile {

		// public TestCauldron() {
		// }
		public override void PlaceInWorld(int i, int j, Item tileItem) {
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
			TileObjectData.newTile.Origin = new Point16(1, 2);
			// For some reason, it's to be
			TileObjectData.newTile.DrawYOffset = 8;

			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TECauldron>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);

			TileObjectData.addAlternate(0);
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
			ModContent.GetInstance<TECauldron>()?.Kill(i, j);
			Item.NewItem(null, i * 16, j * 16, 32, 16, ModContent.ItemType<Items.Placeables.TestCauldron>());
		}
		public override void MouseOver(int i, int j) {
			Player player = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<Items.Placeables.TestCauldron>();
			player.noThrow = 2;
		}
		public override bool RightClick(int i, int j) {
			HelpMe.GetTileEntity<TECauldron>(i, j)?.RightClick(i, j);
			// var ply = Main.LocalPlayer;
			// int slot = ply.selectedItem;
			// var inv = ply.inventory;
			// _crafting.Interract(i, j, ply, inv, slot);
			return base.RightClick(i, j);
		}
	}
}
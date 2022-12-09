using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace TWitchery.Tiles;
public class Pedestal : ModTile {
	public const int magicDrawOffsetY = 2;
	public static readonly Vector2 particleOrigin = new Vector2(8, magicDrawOffsetY - 8);
	public override void PlaceInWorld(int i, int j, Item tileItem) {}
	public override void SetStaticDefaults() {
		Main.tileSolidTop[Type] = false;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileTable[Type] = false;
		Main.tileLavaDeath[Type] = false;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
		// tile heights, from top to bottom. Or not?..
		TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
		// IMPORTANT TO STATE
		TileObjectData.newTile.CoordinatePadding = 0;
		TileObjectData.newTile.Origin = new Point16(0, 1);
		// For some reason, it's to be
		TileObjectData.newTile.DrawYOffset = magicDrawOffsetY;

		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TEPedestal>().Hook_AfterPlacement, -1, 0, false);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);

		TileObjectData.addAlternate(0);
		TileObjectData.addTile(Type);

		ModTranslation name = CreateMapEntryName();
		name.SetDefault(Items.Placeables.Pedestal.DisplayNameText);
		AddMapEntry(new Color(200, 200, 200), name);
		// disableSmartCursor = true;
		AdjTiles = new int[] { };
		MinPick = 0;
		Main.tileCut[Type] = false;
	}
	public override void NumDust(int i, int j, bool fail, ref int num) {
		num = fail ? 1 : 3;
	}
	public override void KillMultiTile(int i, int j, int frameX, int frameY) {
		ModContent.GetInstance<TEPedestal>()?.Kill(i, j);
		Item.NewItem(null, i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Placeables.Pedestal>());
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch) {
		var self = HelpMe.GetTileEntity<TEPedestal>(i, j);
		if (self == null)
			return;
		if (new Point16(i, j) != HelpMe.GetTileTextureOrigin(new Point16(i, j)))
			return;
		self.ItemDrawer.Draw(spriteBatch, new Vector2(i, j) * 16 + new Vector2(8, 10 + magicDrawOffsetY / 2), new Vector2(.5f, 1f));
		base.PostDraw(i, j, spriteBatch);
	}

	public override void MouseOver(int i, int j) {
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		player.cursorItemIconEnabled = true;
		player.cursorItemIconID = ModContent.ItemType<Items.Placeables.Pedestal>();
		player.noThrow = 2;
	}
	public override bool RightClick(int i, int j) {
		HelpMe.GetTileEntity<TEPedestal>(i, j)?.RightClick(i, j);
		return base.RightClick(i, j);
	}
}
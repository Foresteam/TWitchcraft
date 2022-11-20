using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using Terraria.ObjectData;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace TWitchery.Tiles;
using Liquids;
public class Cauldron : ModTile {
	const int magicDrawOffsetY = 8;
	private Asset<Texture2D> _liquidTexture, _liquidSecondaryTexture;
	public override void PlaceInWorld(int i, int j, Item tileItem) {}
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
		TileObjectData.newTile.DrawYOffset = magicDrawOffsetY;

		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TECauldron>().Hook_AfterPlacement, -1, 0, false);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);

		TileObjectData.addAlternate(0);
		TileObjectData.addTile(Type);

		ModTranslation name = CreateMapEntryName();
		name.SetDefault(Items.Placeables.Cauldron.DisplayNameText);
		AddMapEntry(new Color(200, 200, 200), name);           
		// disableSmartCursor = true;
		AdjTiles = new int[] { TileID.CookingPots };
		MinPick = 0;
		Main.tileCut[Type] = false;

		_liquidTexture = ModContent.Request<Texture2D>("TWitchery/Assets/CauldronLiquid");
		_liquidSecondaryTexture = ModContent.Request<Texture2D>("TWitchery/Assets/CauldronLiquidSecondary");
}
	public override void NumDust(int i, int j, bool fail, ref int num) {
		num = fail ? 1 : 3;
	}
	public override void KillMultiTile(int i, int j, int frameX, int frameY) {
		ModContent.GetInstance<TECauldron>()?.Kill(i, j);
		Item.NewItem(null, i * 16, j * 16, 32, 16, ModContent.ItemType<Items.Placeables.Cauldron>());
	}
	public override void MouseOver(int i, int j) {
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		player.cursorItemIconEnabled = true;
		player.cursorItemIconID = ModContent.ItemType<Items.Placeables.Cauldron>();
		player.noThrow = 2;
		var cauldron = HelpMe.GetTileEntity<TECauldron>(i, j);
		if (cauldron != null)
			UISystem.OpenCauldronHoverUI(cauldron.Inventory, cauldron.LiquidInventory);
	}
	public override bool RightClick(int i, int j) {
		HelpMe.GetTileEntity<TECauldron>(i, j)?.RightClick(i, j);
		return base.RightClick(i, j);
	}
	#nullable enable
	private Color? Blend(IEnumerable<Liquid> liquids, Func<Liquid, Color?> colorGetter) {
		if (liquids.Count() == 0)
			return null;
		Color? color = colorGetter(liquids.First());
		if (color == null)
			return null;
		float accumulated = liquids.First().Volume;
		foreach (var liquid in liquids) {
			Color? other = colorGetter(liquid);
			if (other == null || other == color)
				continue;
			else {
				color = HelpMe.Blend((Color)color, (Color)other, accumulated / liquid.Volume / (accumulated == liquids.First().Volume ? 2 : 1));
				accumulated += liquid.Volume;
			}
		}
		return color;
	}
	public override void PostDraw(int i, int j, SpriteBatch spriteBatch) {
		Point16 point = new Point16(i, j);
		Point16 origin = HelpMe.GetTileOrigin(point);
		Point16 dorigin = point - origin;

		Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
		if (Main.drawToScreen)
			zero = Vector2.Zero;
		
		var liquids = HelpMe.GetTileEntity<TECauldron>(i, j).LiquidInventory.GetAll();
		Color? primaryColor = Blend(liquids, liquid => liquid.Color);
		Color? secondaryColor = Blend(liquids, liquid => liquid.ColorSecondary);

		var draw = (Texture2D texture, Color color) => spriteBatch.Draw(
			texture,
			origin.ToVector2() * 16 - Main.screenPosition + zero,
			new Rectangle(dorigin.X * 16, dorigin.Y * 16, 16, 16),
			color,
			0f,
			-dorigin.ToVector2() * 16 + new Vector2(0, magicDrawOffsetY),
			1,
			SpriteEffects.None,
			0f
		);
		if (primaryColor != null)
			draw(_liquidTexture.Value, (Color)primaryColor);
		if (secondaryColor != null)
			draw(_liquidSecondaryTexture.Value, (Color)secondaryColor);
	}
}
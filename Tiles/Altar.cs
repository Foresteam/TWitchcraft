using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using TWitchery.Ext;

namespace TWitchery.Tiles;
#nullable enable
public class Altar : ModTile {
	private static int _dissolveParticle;
	public const int magicDrawOffsetY = 2;
	public static readonly Vector2 particleOrigin = new Vector2(16, 0);
	public override void PlaceInWorld(int i, int j, Item tileItem) { }
	public override void SetStaticDefaults() {
		Main.tileSolidTop[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileTable[Type] = false;
		Main.tileLavaDeath[Type] = false;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
		// tile heights, from top to bottom. Or not?..
		TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
		// IMPORTANT TO STATE
		TileObjectData.newTile.CoordinatePadding = 0;
		TileObjectData.newTile.Origin = new Point16(0, 0);
		// For some reason, it's to be
		TileObjectData.newTile.DrawYOffset = magicDrawOffsetY;

		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TEAltar>().Hook_AfterPlacement, -1, 0, false);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);

		TileObjectData.addAlternate(0);
		TileObjectData.addTile(Type);

		ModTranslation name = CreateMapEntryName();
		name.SetDefault(Items.Placeables.Altar.DisplayNameText);
		AddMapEntry(new Color(200, 200, 200), name);
		// disableSmartCursor = true;
		AdjTiles = new int[] { };
		MinPick = 0;
		Main.tileCut[Type] = false;

		_dissolveParticle = ModContent.DustType<Dusts.Dissolve>();
	}
	public override void NumDust(int i, int j, bool fail, ref int num) {
		num = fail ? 1 : 3;
	}
	public override void KillMultiTile(int i, int j, int frameX, int frameY) {
		ModContent.GetInstance<TEAltar>()?.Kill(i, j);
		Item.NewItem(null, i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Placeables.Altar>());
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch) {
		var self = HelpMe.GetTileEntity<TEAltar>(i, j);
		if (self == null)
			return;
		if (new Point16(i, j) != HelpMe.GetTileTextureOrigin(new Point16(i, j)))
			return;
		self.ItemDrawer.Draw(spriteBatch, new Vector2(i, j) * 16 + particleOrigin, new Vector2(.5f, 1f));
		base.PostDraw(i, j, spriteBatch);
	}
	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData) {
		base.DrawEffects(i, j, spriteBatch, ref drawData);
		var self = HelpMe.GetTileEntity<TEAltar>(i, j);
		if (self == null)
			return;
		if (new Point16(i, j) != HelpMe.GetTileTextureOrigin(new Point16(i, j)))
			return;
		// to be finished...
		// don't know why origin+12 is not actually the tile already, btw
		if (self.HighlightCrafted)
			for (int k = -16; k < 12; k += 4) {
				int dust = Dust.NewDust(new Vector2(i, j) * 16 + particleOrigin + new Vector2(k, -magicDrawOffsetY), 4, 4, DustID.IceTorch, 0, -3, 100, Color.White, 1.3f);
				Main.dust[dust].velocity.X *= .16f;
				Main.dust[dust].velocity.Y = -Math.Abs(Main.dust[dust].velocity.Y * 1.5f);
				Main.dust[dust].noGravity = true;
			}
		if (self.Combining) {
			var pedestalOrigins = TEAltar.GetPedestalsOrigins(i, j);
			var altarPos = HelpMe.GetTileTextureOrigin(new Point16(i, j)).ToVector2() * 16 + particleOrigin + new Vector2(0, -16);
			if (pedestalOrigins != null)
				foreach (var pedestalOrigin in pedestalOrigins) {
					var pedestal = HelpMe.GetTileEntity<TEPedestal>(pedestalOrigin);
					if (pedestal == null || pedestal.Inventory.SlotsUsed == 0)
						continue;
					var pedestalPos = pedestalOrigin.ToVector2() * 16 + Pedestal.particleOrigin;
					var vel = altarPos - pedestalPos;
					var color = pedestal.Inventory.Slot.GetMainColor(1);
					var altcolor = pedestal.Inventory.Slot.GetMainColor(0);
					if (color.R < altcolor.R && color.G < altcolor.G && color.B < altcolor.B)
						color = altcolor;
					for (int k = 0; k < Math.Max(1, (Math.Abs(pedestal.Inventory.Slot.rare) + Math.Min(10, pedestal.Inventory.Slot.stack)) / 2); k++) {
						var randomItemShift = new Vector2(Main.rand.Next(-8, 4), Main.rand.Next(-4, 8));
						int dust = Dust.NewDust(pedestalPos + randomItemShift, 4, 4, _dissolveParticle, 0, 0, 100, color, .6f);
						Main.dust[dust].customData = vel.X > vel.Y ? new Vector2(altarPos.X, 0) : new Vector2(0, altarPos.Y);
						Main.dust[dust].velocity = vel / 16 * 1.2f;
						Main.dust[dust].noGravity = true;
					}
				}
		}
	}

	public override void MouseOver(int i, int j) {
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		player.cursorItemIconEnabled = true;
		player.cursorItemIconID = ModContent.ItemType<Items.Placeables.Altar>();
		player.noThrow = 2;
	}
	private Task<bool>? _blockingRightClick;
	public override bool RightClick(int i, int j) {
		if (!_blockingRightClick?.IsCompleted ?? false)
			return _blockingRightClick?.IsCompleted ?? false ? _blockingRightClick?.Result ?? false : base.RightClick(i, j);
		// hey bro, nice wrap
		Task.Factory.StartNew(() =>
			_blockingRightClick = HelpMe.GetTileEntity<TEAltar>(i, j)?.RightClick(i, j)
		).ConfigureAwait(false);
		return _blockingRightClick?.Result ?? true;
	}
}
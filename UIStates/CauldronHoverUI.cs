using System.Linq;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.UI;
using ReLogic.Graphics;

namespace TWitchery.UIStates;
class CauldronHoverUI : UIState {
	public readonly Cauldron.Inventory inventory;
	public readonly LiquidInventory liquidInventory;
	public readonly Texture2D liquidTexture, liquidSecondaryTexture, liquidBarTexture;

	public CauldronHoverUI(Cauldron.Inventory inventory, LiquidInventory liquidInventory) {
		this.inventory = inventory;
		this.liquidInventory = liquidInventory;
		liquidTexture = ModContent.Request<Texture2D>("TWitchery/Assets/Liquid").Value;
		liquidSecondaryTexture = ModContent.Request<Texture2D>("TWitchery/Assets/LiquidSecondary").Value;
		liquidBarTexture = ModContent.Request<Texture2D>("TWitchery/Assets/LiquidBar").Value;
	}

	private Rectangle DrawItem(SpriteBatch spriteBatch, int index, Item item, Color color) {
		const float baseScale = 1f;
		const float itemsPadding = 20 * baseScale;
		const int yOffset = -40;
		const float spacing = 0;
		const float maxSize = 20 * baseScale;
		const float itemSlotRatio = 1.5f;

		float drawScale = baseScale * 1f;
		float column = (maxSize + itemsPadding + spacing) * index;

		Main.instance.LoadItem(item.type); // load item before trying to get its texture (Item only gets loaded once)
		Texture2D itemTexture = TextureAssets.Item[item.type].Value; // get item texture
		Vector2 drawPos = new(Main.MouseScreen.X + column, Main.MouseScreen.Y + yOffset);

		// draw slot background
		Vector2 backgroundPos = new(drawPos.X - maxSize + 2, drawPos.Y - maxSize + 2);
		spriteBatch.Draw(TextureAssets.InventoryBack.Value, backgroundPos, null, color, 0, new Vector2(), drawScale * .7f, SpriteEffects.None, 0);
		// handle animation frames
		int frameCount = 1;
		Rectangle itemFrameRect = itemTexture.Frame();
		if (Main.itemAnimations[item.type] != null) {
			itemFrameRect = Main.itemAnimations[item.type].GetFrame(itemTexture);
			frameCount = Main.itemAnimations[item.type].FrameCount;
		}

		// handle draw scale
		if (itemTexture.Width > maxSize || itemTexture.Height / frameCount > maxSize)
			drawScale = maxSize / Math.Max(itemFrameRect.Height, itemFrameRect.Width);
		else
			drawScale = baseScale * 1f;

		// draw item texture
		Vector2 itemSize = itemFrameRect.Size() * drawScale;
		Vector2 itemPos = drawPos - itemFrameRect.Size() * drawScale / 2;
		spriteBatch.Draw(itemTexture, itemPos, itemFrameRect, Color.White, 0, new Vector2(), drawScale, SpriteEffects.None, 0);

		// draw stack text
		if (item.stack > 1) {
			Vector2 corner = (drawPos - backgroundPos);
			corner.X *= .8f;
			corner.Y *= .2f;
			corner.X = -corner.X;
			Vector2 textPos = drawPos + corner;
			Utils.DrawBorderString(spriteBatch, item.stack.ToString(), textPos, Color.White, Main.inventoryScale);
		}

		var rsRect = TextureAssets.InventoryBack.Frame();
		rsRect = new Rectangle(
			(int)backgroundPos.X,
			(int)backgroundPos.Y,
			(int)(rsRect.Width * drawScale * .7f),
			(int)(rsRect.Height * drawScale * .7f)
		);
		return rsRect;
	}
	private void DrawItems(SpriteBatch spriteBatch) {
		Item[] items = inventory.slots;
		int i = 0;
		for (; i < items.Length; i++)
			DrawItem(spriteBatch, i, items[i], Color.White);
		DrawItem(spriteBatch, i++, inventory.catalyst, Color.Red);
	}
	private void DrawLiquidBar(SpriteBatch spriteBatch, Vector2 position, int length) {
		const int height = 14;
		int leftOffset = 0;
		spriteBatch.Draw(
			liquidBarTexture,
			position - new Vector2(1, 1),
			new Rectangle(0, 0, length + 2, height + 2),
			Color.Black
		);
		foreach (var liquid in liquidInventory) {
			var xsize = (int)(liquid.Volume / liquidInventory.volume * length);
			spriteBatch.Draw(
				liquidTexture,
				position + new Vector2(leftOffset, 0),
				new Rectangle(leftOffset, 0, (int)xsize, height),
				liquid.Color
			);
			if (liquid.ColorSecondary != null)
				spriteBatch.Draw(
					liquidSecondaryTexture,
					position + new Vector2(leftOffset, 0),
					new Rectangle(leftOffset, 0, (int)xsize, height),
					(Color)liquid.ColorSecondary
				);
			leftOffset += (int)xsize;
		}
	}
	private void DrawLiquidStatus(SpriteBatch spriteBatch, Vector2 position) {
		if (liquidInventory.GetAll().Count == 0)
			return;
		int offset = 0;
		foreach (var liquid in liquidInventory.GetAll().AsEnumerable().Reverse()) {
			spriteBatch.DrawString(FontAssets.MouseText.Value, $"{liquid.Name}, {liquid.Volume}B", position + new Vector2(0, offset), Color.White);
			offset -= 15;
		}
	}
	public override void Draw(SpriteBatch spriteBatch) {
		base.Draw(spriteBatch);

		if (inventory == null)
			return;

		DrawItems(spriteBatch);
		var barPos = Main.MouseScreen + new Vector2(-15, -75);
		DrawLiquidBar(spriteBatch, barPos, 230);
		DrawLiquidStatus(spriteBatch, barPos - new Vector2(0, 24));
	}
}
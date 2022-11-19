using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;
using TWitchery.Cauldron;

namespace TWitchery.UIStates;
class CauldronHoverUI : UIState {
	public readonly CauldronInventory inventory;
	public readonly LiquidInventory liquidInventory;

	public CauldronHoverUI(CauldronInventory inventory, LiquidInventory liquidInventory) {
		this.inventory = inventory;
		this.liquidInventory = liquidInventory;
	}

	private void DrawItem(SpriteBatch spriteBatch, int index, Item item, Color color) {
		const float baseScale = 1f;
		const int yOffset = -40;
		const float padding = 20 * baseScale;
		const float spacing = 0;
		const float maxSize = 20 * baseScale;
		const float itemSlotRatio = 1.5f;

		float drawScale = baseScale * 1f;
		float column = (maxSize + padding + spacing) * index;

		Main.instance.LoadItem(item.type); // load item before trying to get its texture (Item only gets loaded once)
		Texture2D itemTexture = TextureAssets.Item[item.type].Value; // get item texture
		Vector2 drawPos = new(Main.MouseScreen.X + column, Main.MouseScreen.Y + yOffset);

		// draw slot background
		Vector2 backgroundPos = new(drawPos.X - maxSize + 2, drawPos.Y - maxSize + 2);
		spriteBatch.Draw(TextureAssets.InventoryBack.Value, backgroundPos, null, color, 0, new Vector2(), drawScale * .7f, SpriteEffects.None, 0);
		// handle animation frames
		int frameCount = 1;
		Rectangle itemFrameRect = itemTexture.Frame();
		if (Main.itemAnimations[item.type] != null)
		{
			itemFrameRect = Main.itemAnimations[item.type].GetFrame(itemTexture);
			frameCount = Main.itemAnimations[item.type].FrameCount;
		}

		// handle draw scale
		if (itemTexture.Width > maxSize || itemTexture.Height / frameCount > maxSize) {
			drawScale = maxSize * itemSlotRatio / (float)(itemFrameRect.Width <= itemFrameRect.Height ?
				itemFrameRect.Height :
				itemFrameRect.Width);
		}

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
	}
	private void DrawLiquidBar(SpriteBatch spriteBatch, Vector2 offset, float length) {
		float leftOffset = 0;
		foreach (var liquid in liquidInventory) {
			var xsize = liquid.Volume / liquidInventory.volume * length;
			spriteBatch.Draw(
				TextureAssets.Liquid[Terraria.ID.LiquidID.Water].Value,
				offset + new Vector2(leftOffset, 0),
				new Rectangle(0, 0, (int)xsize, 10),
				liquid.Color,
				0,
				new Vector2(),
				length / TextureAssets.Liquid[Terraria.ID.LiquidID.Water].Frame().Width,
				SpriteEffects.None,
				0
			);
			leftOffset += (int)xsize;
		}
	}
	public override void Draw(SpriteBatch spriteBatch) {
		base.Draw(spriteBatch);

		if (inventory == null)
			return;

		// all items in the chest
		Item[] items = inventory.slots;

		int i = 0;
		for (; i < items.Length; i++)
			DrawItem(spriteBatch, i, items[i], Color.White);
		DrawItem(spriteBatch, i++, inventory.catalyst, Color.Red);
		DrawLiquidBar(spriteBatch, Main.MouseScreen + new Vector2(0, -120), 300);
	}
}
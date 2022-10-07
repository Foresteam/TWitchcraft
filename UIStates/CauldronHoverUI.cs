using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace TWitchery.UIStates;
class CauldronHoverUI : UIState {
	public readonly StackedInventory inventory;

	public CauldronHoverUI(StackedInventory inventory) {
		this.inventory = inventory;
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
	private void DrawLiquidBar(Vector2 offset, Liquid[] current, float max) {}
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
	}
}
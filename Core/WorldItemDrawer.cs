using System.Collections.Generic;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TWitchery;
class WorldItemDrawer {
	private readonly Func<Item> GetSlot;
	public WorldItemDrawer(Func<Item> slot) {
		GetSlot = slot;
	}
	/**
	<summary>Draws item texture in world</summary>
	<param name="origin">Percentage origin. Defaults to center</param>
	**/
	public void Draw(SpriteBatch spriteBatch, Vector2 worldPos, Vector2? origin = null, float size = 24, float scale = 1f) {
		var item = GetSlot();
		if (item == null || item.type == 0)
			return;

		Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
		if (Main.drawToScreen)
			zero = Vector2.Zero;
		var screenPos = worldPos - Main.screenPosition + zero;

		if (origin == null)
			origin = new Vector2(.5f, .5f);
		var norigin = (Vector2)origin;
		float maxSize = size * scale;
		float drawScale;
		Main.instance.LoadItem(item.type);

		var texture = TextureAssets.Item[item.type].Value;
		var frameRect = texture.Frame();
		// settle draw scale
		if (Math.Max(texture.Width, texture.Height) > maxSize)
			drawScale = maxSize / Math.Max(frameRect.Height, frameRect.Width);
		else
			drawScale = scale;
		
		var finalSize = frameRect.Size() * drawScale;
		var finalPos = screenPos - new Vector2(norigin.X * finalSize.X, norigin.Y * finalSize.Y);
		spriteBatch.Draw(texture, finalPos, frameRect, Color.White, 0, new Vector2(), drawScale, SpriteEffects.None, 0);
	}
}
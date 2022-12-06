using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Microsoft.Xna.Framework;

namespace TWitchery.Ext;
static class ItemExt {
	public static Color GetMainColor(this Item item, int nth = 0) {
		if (item.type >= TextureAssets.Item.Length)
			return Color.Black;
		var texture = TextureAssets.Item[item.type].Value;
		var pixels = texture.GetPixels();
		Dictionary<Color, int> colors = new();
		foreach (var pixel in pixels) {
			if (pixel.A < 50)
				continue;
			if (!colors.ContainsKey(pixel))
				colors[pixel] = 0;
			colors[pixel]++;
		}
		var rs = colors.ToList();
		rs.Sort((a, b) => a.Value.CompareTo(b.Value));
		return rs[rs.Count - 1 - (rs.Count >= nth ? nth : 0)].Key;
	}
}
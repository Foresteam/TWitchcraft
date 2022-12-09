using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TWitchery.ExtTexture2D;
static class Texture2DExt {
	public static Color[] GetPixels(this Texture2D texture) {
		Color[] colors1d = new Color[texture.Width * texture.Height];
		texture.GetData<Color>(colors1d);
		return colors1d;
	}
	public static Color[,] GetPixels2D(this Texture2D texture) {
		var colors1d = texture.GetPixels();
		var colors2d = new Color[texture.Width, texture.Height];
		for (int i = 0; i < texture.Width; i++)
			for (int j = 0; j < texture.Height; j++)
				colors2d[j, i] = colors1d[j + (i * texture.Width)];
		return colors2d;
	}
}
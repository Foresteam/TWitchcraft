using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TWitchery.Dusts;
public class Bubble : ModDust {
	private float _initialScale;
	public override void OnSpawn(Dust dust) {
		dust.noGravity = true;
		dust.frame = new Rectangle(0, 0, 30, 30);
		_initialScale = dust.scale;
		// If our texture had 3 different dust on top of each other (a 30x90 pixel image), we might do this:
		// dust.frame = new Rectangle(0, Main.rand.Next(3) * 30, 30, 30);
	}

	public override bool Update(Dust dust) {
		// Move the dust based on its velocity and reduce its size to then remove it, as the 'return false;' at the end will prevent vanilla logic.
		dust.position += dust.velocity;
		dust.scale -= 0.01f * _initialScale;

		if (dust.scale < _initialScale * 0.75f)
			dust.active = false;

		return false;
	}
}
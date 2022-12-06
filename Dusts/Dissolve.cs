using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TWitchery.Dusts;
#nullable enable
public class Dissolve : ModDust {
	private float _initialScale;
	private bool[]? _destroyPastState;
	public override void OnSpawn(Dust dust) {
		_destroyPastState = null;
		dust.noGravity = true;
		dust.frame = new Rectangle(0, 0, 15, 15);
		_initialScale = dust.scale;
		// If our texture had 3 different dust on top of each other (a 30x90 pixel image), we might do this:
		// dust.frame = new Rectangle(0, Main.rand.Next(3) * 30, 30, 30);
	}

	public override bool Update(Dust dust) {
		// Move the dust based on its velocity and reduce its size to then remove it, as the 'return false;' at the end will prevent vanilla logic.
		dust.position += dust.velocity;
		dust.scale -= 0.02f * _initialScale;

		if (dust.scale < _initialScale * 0.3f)
			dust.active = false;
		Vector2? destroyPast = (Vector2?)dust.customData;
		if (destroyPast != null) {
			var nstate = new bool[] { dust.position.X >= ((Vector2)destroyPast).X, dust.position.Y >= ((Vector2)destroyPast).Y };
			if (_destroyPastState != null && (_destroyPastState[0] != nstate[0] || _destroyPastState[1] != nstate[1]))
				dust.active = false;
			_destroyPastState = nstate;
		}

		return false;
	}
}
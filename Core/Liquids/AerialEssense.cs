using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class AerialEssense : Liquid {
	public override Color Color => new FeatherfallPotion().Color;
	public override Color? ColorSecondary => new Color(255, 255, 255);
	public AerialEssense(float volume = 0) : base(volume) {}
}
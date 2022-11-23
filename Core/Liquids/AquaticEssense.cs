using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class AquaticEssense : Liquid {
	public override Color Color => new GillsPotion().Color;
	public override Color? ColorSecondary => new Color(0, 0, 0);
	public AquaticEssense(float volume = 0) : base(volume) {}
}
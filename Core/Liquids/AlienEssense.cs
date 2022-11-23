using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class AlienEssense : Liquid {
	public override Color Color => new GravitationPotion().Color;
	public override Color? ColorSecondary => new Color(0, 0, 0);
	public AlienEssense(float volume = 0) : base(volume) {}
}
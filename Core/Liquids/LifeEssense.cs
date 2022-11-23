using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class LifeEssense : Liquid {
	public override Color Color => new LifeforcePotion().Color;
	public override Color? ColorSecondary => new LifeforcePotion().ColorSecondary;
	public LifeEssense(float volume = 0) : base(volume) {}
}
using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class EssenseOfForce : Liquid {
	public override Color Color => new ThornsPotion().Color;
	public override Color? ColorSecondary => new Color(30, 30, 30);
	public EssenseOfForce(float volume = 0) : base(volume) {}
}
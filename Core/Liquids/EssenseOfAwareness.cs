using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class EssenseOfAwareness : Liquid {
	public override Color Color => new EndurancePotion().Color;
	public override Color? ColorSecondary => new Color(15, 30, 15);
	public EssenseOfAwareness(float volume = 0) : base(volume) {}
}
using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class EssenseOfDarkness : Liquid {
	public override Color Color => new MiningPotion().Color;
	public override Color? ColorSecondary => new Color(20, 20, 20);
	public EssenseOfDarkness(float volume = 0) : base(volume) {}
}
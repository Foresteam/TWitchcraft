using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class EssenseOfAvarice : Liquid {
	public override Color Color => new SpelunkerPotion().Color;
	public override Color? ColorSecondary => new Color(10, 10, 10);
	public EssenseOfAvarice(float volume = 0) : base(volume) {}
}
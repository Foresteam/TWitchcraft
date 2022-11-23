using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class LovePotion : Liquid {
	public override string Name => "Love potion";
	public override Color Color => new Color(138, 1, 0);
	public override Color? ColorSecondary => new Color(255, 186, 207);
	public LovePotion(float volume = 0) : base(volume) {}
}
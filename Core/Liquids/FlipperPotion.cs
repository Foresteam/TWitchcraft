using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class FlipperPotion : Liquid {
	public override string Name => "Flipper potion";
	public override Color Color => new Color(0, 71, 111);
	public override Color? ColorSecondary => new Color(74, 171, 223);
	public FlipperPotion(float volume) : base(volume) {}
}
using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class GenderChangePotion : Liquid {
	public override string Name => "Gender change potion";
	public override Color Color => new Color(161, 130, 195);
	public override Color? ColorSecondary => new Color(118, 168, 215);
	public GenderChangePotion(float volume = 0) : base(volume) {}
}
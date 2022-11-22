using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class GravitationPotion : Liquid {
	public override string Name => "Gravitation potion";
	public override Color Color => new Color(107, 31, 191);
	public override Color? ColorSecondary => new Color(157, 98, 238);
	public GravitationPotion(float volume) : base(volume) {}
}
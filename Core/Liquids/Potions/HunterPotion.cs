using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class HunterPotion : Liquid {
	public override string Name => "Hunter potion";
	public override Color Color => new Color(241, 130, 47);
	public override Color? ColorSecondary => new Color(134, 59, 2);
	public HunterPotion(float volume = 0) : base(volume) {}
}
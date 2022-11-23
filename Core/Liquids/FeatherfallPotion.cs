using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class FeatherfallPotion : Liquid {
	public override string Name => "Featherfall potion";
	public override Color Color => new Color(47, 179, 223);
	public override Color? ColorSecondary => new Color(44, 190, 234);
	public FeatherfallPotion(float volume = 0) : base(volume) {}
}
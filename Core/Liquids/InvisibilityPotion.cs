using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class InvisibilityPotion : Liquid {
	public override string Name => "Invisibility potion";
	public override Color Color => new Color(5, 104, 112);
	public override Color? ColorSecondary => new Color(47, 204, 217);
	public InvisibilityPotion(float volume) : base(volume) {}
}
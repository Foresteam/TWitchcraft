using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class RestorationPotion : Liquid {
	public override string Name => "Restoration potion";
	public override Color Color => new Color(191, 37, 157);
	public override Color? ColorSecondary => new Color(255, 174, 249);
	public RestorationPotion(float volume) : base(volume) {}
}
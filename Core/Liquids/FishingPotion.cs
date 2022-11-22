using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class FishingPotion : Liquid {
	public override string Name => "Fishing potion";
	public override Color Color => new Color(40, 168, 95);
	public override Color? ColorSecondary => new Color(22, 140, 72);
	public FishingPotion(float volume) : base(volume) {}
}
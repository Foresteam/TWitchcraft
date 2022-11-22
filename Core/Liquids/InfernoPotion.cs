using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class InfernoPotion : Liquid {
	public override string Name => "Inferno potion";
	public override Color Color => new Color(254, 228, 0);
	public override Color? ColorSecondary => new Color(228, 63, 14);
	public InfernoPotion(float volume) : base(volume) {}
}
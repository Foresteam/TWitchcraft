using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class EndurancePotion : Liquid {
	public override string Name => "Endurance potion";
	public override Color Color => new Color(76, 76, 74);
	public override Color? ColorSecondary => new Color(165, 166, 163);
	public EndurancePotion(float volume) : base(volume) {}
}